class ArrayMemento:
    """Memento: Знімок стану масиву для реалізації Undo"""
    def __init__(self, state):
        self._state = list(state)
    def get_state(self):
        return self._state

class CustomArray:
    """Колекція, що підтримує патерн Ітератор та роботу з Memento"""
    def __init__(self, data=None):
        self._data = data if data else []

    def __iter__(self):
        # Реалізація патерну Ітератор
        return iter(self._data)
    
    def get_raw_list(self):
        return self._data
    
    def set_raw_list(self, data):
        self._data = data

    def save(self) -> ArrayMemento:
        return ArrayMemento(self._data)

    def restore(self, memento: ArrayMemento):
        self._data = memento.get_state()