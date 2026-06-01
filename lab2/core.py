import random
from models import CustomArray
from config import SettingsManager
from algorithms import (
    MetricsDecorator,
    BubbleSort,
    InsertionSort,
    SelectionSort,
    QuickSort,
    BuiltInSortAdapter
)
class StatsSubject:
    """Observer Subject: Посередник для розсилки повідомлень про аналітику"""
    def __init__(self):
        self._observers = []

    def attach(self, observer):
        self._observers.append(observer)

    def notify(self, visual_updates, algorithm_name):
        for observer in self._observers:
            observer.on_stats_update(visual_updates, algorithm_name)

class SortFacade:
    """Facade: Головний пульт управління підсистемами для інтерфейсу"""
    def __init__(self, ui_observer):
        self.settings = SettingsManager()
        self.current_array = CustomArray()
        self._memento_history = []
        
        self.subject = StatsSubject()
        self.subject.attach(ui_observer)
        self.generate_new_array()

    def generate_new_array(self):
        size = self.settings.array_size
        new_data = [random.randint(10, 100) for _ in range(size)]
        self.current_array.set_raw_list(new_data)
        self._memento_history.clear()
        self.save_state()

    def save_state(self):
        self._memento_history.append(self.current_array.save())

    def undo(self):
        if len(self._memento_history) > 1:
            self._memento_history.pop() 
            last_memento = self._memento_history[-1]
            self.current_array.restore(last_memento)
            return True
        return False

    def execute_sorting(self, sort_type: str, callback=None):
        if callback is None:
            callback = self.save_state
            
        self.save_state() 
        sorter = SortFactory.create_sorter(sort_type)
        sorter.sort(self.current_array, callback)
        
        # Передаємо лічильник операцій замість часу
        self.subject.notify(sorter.visual_updates, sort_type)

class SortCommand:
    """Command: Об'єкт-запит на запуск сортування"""
    def __init__(self, facade: SortFacade, sort_type: str, callback):
        self.facade = facade
        self.sort_type = sort_type
        self.callback = callback

    def execute(self):
        self.facade.execute_sorting(self.sort_type, self.callback)

class SortFactory:
    """Factory Method: Фабрика створення об'єктів сортування"""
    @staticmethod
    def create_sorter(sort_type: str):
        if sort_type == "Bubble":
            return MetricsDecorator(BubbleSort())
        elif sort_type == "Insertion":
            return MetricsDecorator(InsertionSort())
        elif sort_type == "Selection":
            return MetricsDecorator(SelectionSort())
        elif sort_type == "Quick":
            return MetricsDecorator(QuickSort())
        elif sort_type == "Built-in":
            return MetricsDecorator(BuiltInSortAdapter())
        raise ValueError("Unknown sort type")

