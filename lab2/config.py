class SettingsManager:
    """Singleton: Глобальні налаштування програми"""
    _instance = None
    def __new__(cls):
        if cls._instance is None:
            cls._instance = super(SettingsManager, cls).__new__(cls)
            cls._instance.array_size = 40
            cls._instance.delay = 0.02  # Швидкість анімації
        return cls._instance