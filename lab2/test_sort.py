import unittest
from models import CustomArray
from algorithms import BubbleSort, QuickSort
from core import SortFactory

class TestSortingSystem(unittest.TestCase):
    def setUp(self):
        self.array = CustomArray()
        self.array.set_raw_list([5, 3, 8, 1, 2])

    def test_bubble_sort_correctness(self):
        """Перевірка коректності сортування бульбашкою"""
        sorter = BubbleSort()
        sorter.sort(self.array)
        self.assertEqual(self.array.get_raw_list(), [1, 2, 3, 5, 8])

    def test_quick_sort_correctness(self):
        """Перевірка коректності швидкого сортування"""
        sorter = QuickSort()
        sorter.sort(self.array)
        self.assertEqual(self.array.get_raw_list(), [1, 2, 3, 5, 8])

    def test_memento_undo(self):
        """Тестування збереження та відновлення стану (Memento)"""
        initial_state = list(self.array.get_raw_list())
        memento = self.array.save()
        
        # Руйнуємо масив
        self.array.set_raw_list([99, 99, 99])
        
        # Відновлюємо з Memento
        self.array.restore(memento)
        self.assertEqual(self.array.get_raw_list(), initial_state)

    def test_factory_creation(self):
        """Перевірка роботи фабричного методу"""
        decorator = SortFactory.create_sorter("Bubble")
        # Перевіряємо, чи фабрика загорнула об'єкт у Декоратор
        self.assertEqual(decorator.__class__.__name__, "MetricsDecorator")

if __name__ == "__main__":
    unittest.main()