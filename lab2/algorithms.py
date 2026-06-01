from models import CustomArray
class BaseSort: 
    """Template Method & Strategy: Абстрактний клас алгоритму сортування"""
    def sort(self, custom_array: CustomArray, callback=None):
        # Скелет алгоритму сортування (Template Method)
        self._before_sort()
        self._execute_sort(custom_array.get_raw_list(), callback)
        self._after_sort()

    def _before_sort(self):
        pass  

    def _execute_sort(self, arr, callback):
        raise NotImplementedError()

    def _after_sort(self):
        pass

class BubbleSort(BaseSort):
    """Strategy 1: Бульбашка"""
    def _execute_sort(self, arr, callback):
        n = len(arr)
        for i in range(n):
            for j in range(0, n-i-1):
                if arr[j] > arr[j+1]:
                    arr[j], arr[j+1] = arr[j+1], arr[j]
                    if callback: callback(arr)

class InsertionSort(BaseSort):
    """Strategy 2: Вставки"""
    def _execute_sort(self, arr, callback):
        for i in range(1, len(arr)):
            key = arr[i]
            j = i-1
            while j >= 0 and key < arr[j] :
                    arr[j + 1] = arr[j]
                    j -= 1
                    if callback: callback(arr)
            arr[j + 1] = key
            if callback: callback(arr)

class SelectionSort(BaseSort):
    """Strategy 3: Вибір"""
    def _execute_sort(self, arr, callback):
        n = len(arr)
        for i in range(n):
            min_idx = i
            for j in range(i+1, n):
                if arr[j] < arr[min_idx]:
                    min_idx = j
            if min_idx != i:
                arr[i], arr[min_idx] = arr[min_idx], arr[i]
                if callback: callback(arr)

class QuickSort(BaseSort):
    """Strategy 4: Швидке сортування (Хоара)"""
    def _execute_sort(self, arr, callback):
        self._quick_sort(arr, 0, len(arr) - 1, callback)

    def _quick_sort(self, arr, low, high, callback):
        if low < high:
            pivot_idx = self._partition(arr, low, high, callback)
            self._quick_sort(arr, low, pivot_idx - 1, callback)
            self._quick_sort(arr, pivot_idx + 1, high, callback)

    def _partition(self, arr, low, high, callback):
        pivot = arr[high]
        i = low - 1
        for j in range(low, high):
            if arr[j] < pivot:
                i += 1
                arr[i], arr[j] = arr[j], arr[i]
                if callback: callback(arr)
        arr[i + 1], arr[high] = arr[high], arr[i + 1]
        if callback: callback(arr)
        return i + 1

class BuiltInSortAdapter(BaseSort):
    """Adapter: Обгортка над стандартним сортуванням Python під наш інтерфейс"""
    def _execute_sort(self, arr, callback):
        sorted_list = sorted(arr)
        for i in range(len(arr)):
            arr[i] = sorted_list[i]
        if callback: callback(arr)

class MetricsDecorator:
    """Decorator: Замість часу підраховує КІЛЬКІСТЬ фактичних змін масиву"""
    def __init__(self, sorter: BaseSort):
        self._sorter = sorter
        self.visual_updates = 0

    def sort(self, custom_array: CustomArray, callback=None):
        self.visual_updates = 0
        
        # Перехоплюємо колбек, щоб інкрементувати лічильник при кожній зміні
        def wrapped_callback(arr):
            self.visual_updates += 1
            if callback:
                callback(arr)
                
        self._sorter.sort(custom_array, wrapped_callback)
