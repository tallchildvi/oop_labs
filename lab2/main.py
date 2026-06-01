import pygame
import time
from config import SettingsManager
from core import SortFacade

class PygameApp:
    def __init__(self):
        pygame.init()
        pygame.font.init()
        self.screen = pygame.display.set_mode((600, 400))
        pygame.display.set_caption("ООП Лабораторна №2 - Аналізатор Сортувань")
        
        self.font = pygame.font.SysFont("Arial", 12)
        self.bold_font = pygame.font.SysFont("Arial", 14, bold=True)
        
        self.settings = SettingsManager()
        self.facade = SortFacade(ui_observer=self)
        
        # Розширений список алгоритмів
        self.algorithms = ["Bubble", "Insertion", "Selection", "Quick", "Built-in"]
        self.current_algo_idx = 0
        self.stats_text = "Статистика: Очікування запуску..."
        self.is_sorting = False

    def on_stats_update(self, visual_updates, algorithm_name):
        """Реалізація методу патерну Observer"""
        self.stats_text = f"Результат: {algorithm_name} сортував масив за {visual_updates} операцій перестановки."

    def redraw(self, snapshot=None):
        """Оновлює кадр і малює стовпчики за допомогою патерну Ітератор"""
        self.screen.fill((255, 255, 255))

        # Верхнє інструктивне меню
        algo_name = self.algorithms[self.current_algo_idx]
        menu_str = f"Алгоритм (клавіші 1-5): {algo_name}  |  [SPACE] - Старт  |  [G] - Генерувати  |  [U] - Undo"
        txt_menu = self.font.render(menu_str, True, (50, 50, 50))
        self.screen.blit(txt_menu, (15, 15))

        # Статистика кроків (Observer дані)
        txt_stats = self.bold_font.render(self.stats_text, True, (0, 90, 160))
        self.screen.blit(txt_stats, (15, 38))

        pygame.draw.rect(self.screen, (220, 220, 220), (10, 65, 580, 320), 1)

        data = snapshot if snapshot is not None else self.facade.current_array
        raw_list = [item for item in data] # Робота патерну Ітератор

        if raw_list:
            max_val = max(raw_list)
            canvas_w = 576
            canvas_h = 315
            bar_width = canvas_w / len(raw_list)

            for i, value in enumerate(raw_list):
                x = 12 + i * bar_width
                bar_h = (value / max_val) * (canvas_h - 20)
                y = 65 + canvas_h - bar_h

                pygame.draw.rect(self.screen, (100, 180, 240), (x, y, bar_width - 2, bar_h))
                pygame.draw.rect(self.screen, (0, 50, 150), (x, y, bar_width - 2, bar_h), 1)

        pygame.display.flip()

    def anim_callback(self, current_state_arr):
        self.redraw(list(current_state_arr))
        time.sleep(self.settings.delay)
        
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                pygame.quit()
                exit()

    def run(self):
        running = True
        self.redraw()
        
        while running:
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    running = False
                    
                elif event.type == pygame.KEYDOWN:
                    if self.is_sorting:
                        continue 
                        
                    if event.key == pygame.K_SPACE:
                        self.is_sorting = True
                        self.stats_text = "Сортування та підрахунок операцій..."
                        
                        cmd = SortCommand(self.facade, self.algorithms[self.current_algo_idx], self.anim_callback)
                        cmd.execute()
                        
                        self.is_sorting = False
                        self.redraw()
                        
                    elif event.key == pygame.K_g:
                        self.facade.generate_new_array()
                        self.stats_text = "Згенеровано новий масив чисел."
                        self.redraw()
                        
                    elif event.key == pygame.K_u:
                        if self.facade.undo():
                            self.stats_text = "Стан масиву відновлено назад (Undo)."
                        else:
                            self.stats_text = "Немає збережених станів для Undo."
                        self.redraw()
                        
                    elif event.key == pygame.K_1:
                        self.current_algo_idx = 0
                        self.stats_text = "Змінено алгоритм: Bubble Sort"
                        self.redraw()
                    elif event.key == pygame.K_2:
                        self.current_algo_idx = 1
                        self.stats_text = "Змінено алгоритм: Insertion Sort"
                        self.redraw()
                    elif event.key == pygame.K_3:
                        self.current_algo_idx = 2
                        self.stats_text = "Змінено алгоритм: Selection Sort"
                        self.redraw()
                    elif event.key == pygame.K_4:
                        self.current_algo_idx = 3
                        self.stats_text = "Змінено алгоритм: Quick Sort"
                        self.redraw()
                    elif event.key == pygame.K_5:
                        self.current_algo_idx = 4
                        self.stats_text = "Змінено алгоритм: Вбудоване сортування Python"
                        self.redraw()

        pygame.quit()

if __name__ == "__main__":
    app = PygameApp()
    app.run()