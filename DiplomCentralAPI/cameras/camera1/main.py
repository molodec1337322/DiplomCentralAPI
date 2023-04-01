"""
    Данный модуль отвечает за записывание и начальное преобразование изображений.
    Модуль действует по следующему принципу:
        0. API получает сообщение с настройками для проведения сессии записи.
        1. Открывается сессия записи (в еденицу времени сессия может быть только одна). Следит за сессиями должно API.
           Если сессию по каким-то причинам невозможно начать, то API возвращает соответсвующее сообщение на CAPI.
        2. Запускается таймер. Таймер - абосолютная величина времени, исходя из которой принимаетются решения
           о начале и конце записи
        3. По необходимости начинается либо сбор изобрадений, либо запись видео. В рамках сбора изображений - они могут
           быть преобразованы в grayscale. К изображениям могут также записываться метаданные (очередность + таймкод).
           Данные файлы - будут считаться временными и будут помечены tmp.
        4. Когда заканчивается захват медиаинформации - останавливается таймер, и информация о ней возвращается в CAPI.
        5. После получения ответа от CAPI о переносе всех необходимых данных - идет удаление всех tmp файлов
           и освобождение сессии.
    prereq для модуля: opencv-python
"""


import cv2
import time
import threading


class TimerError(Exception):
    """A custom exception used to report errors in use of Timer class"""


class Timer:

    def __init__(self):
        self._start_time = None
        self.timer_thread = threading.Thread(target=self.start())

    def start(self):
        """Start a new timer"""
        if self._start_time is not None:
            raise TimerError(f"Timer is running. Use .stop() to stop it")

        self._start_time = time.perf_counter()

    def stop(self):
        """Stop the timer, and report the elapsed time"""
        if self._start_time is None:
            raise TimerError(f"Timer is not running. Use .start() to start it")

        elapsed_time = time.perf_counter() - self._start_time
        self._start_time = None
        print(f"Elapsed time: {elapsed_time:0.4f} seconds")

    def async_start(self):
        self.timer_thread.start()

    def async_stop(self):
        self.stop()
        self.timer_thread.join()


class TimerCallback:

    def __init__(self):
        self._is_time_passed = True
        self._time = 0
        self._timer_thread = None

    def start(self, seconds):
        self._is_time_passed = False
        self._timer_thread = threading.Thread(target=self.wait, args=(seconds,))
        self._timer_thread.start()

    def wait(self, seconds):
        time.sleep(seconds)
        self._is_time_passed = True

    def reset(self):
        self._is_time_passed = True
        if self._timer_thread.is_alive():
            self._timer_thread.join()


def create_pictures(path_to_img: str, grayscale: bool, resize: bool, time_between_screenshots_seconds: int,
                                                amount_of_pictures: int, camera_number=0, resize_x=640, resize_y=480):
    cam = cv2.VideoCapture(camera_number)

    for i in range(amount_of_pictures):
        result, image = cam.read()
        if resize:
            image = resize_picture(resize_x, resize_y, image)
        if grayscale:
            image = img_to_grayscale(image)
        filename = path_to_img + '_' + str(i) + '_' + str(time_between_screenshots_seconds * i) + '_' + 'tmp.jpg'
        cv2.imwrite(filename, image)
        time.sleep(time_between_screenshots_seconds)
    return "ok"


"""cv2.imshow("now", get_current_picture())
# If keyboard interrupt occurs, destroy image
# window
cv2.waitKey(0)
cv2.destroyWindow("now")"""


def record_video(path_to_vid, is_grayscale: bool, resolution_x: int, resolution_y: int, framerate, duration_sec):
    capture = cv2.VideoCapture(0)

    fourcc = cv2.VideoWriter_fourcc('X', 'V', 'I', 'D')
    videoWriter = cv2.VideoWriter(path_to_vid, fourcc, framerate, (resolution_x, resolution_y))

    timer_callback = TimerCallback()
    timer_callback.start(duration_sec)

    while True:
        ret, frame = capture.read()
        if ret:
            videoWriter.write(frame)

        if cv2.waitKey(1) == 27:
            break

        if timer_callback._is_time_passed:
            timer_callback.reset()
            break

    capture.release()
    videoWriter.release()
    return "ok"


def calibrate_camera():
    pass


def resize_picture(to_x: int, to_y: int, img):  # функция для ресайза изображения до (to_x. to_y)
    return cv2.resize(img, (to_x, to_y), cv2.INTER_NEAREST)


def img_to_grayscale(img):  # перевод изображения в Оттенки серого
    return cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)


'''
if __name__ == "__main__":
    """timer = Timer()

    timer.async_start()
    time.sleep(2)
    #get_current_picture()
    #cv2.imshow("now", get_current_picture())
    timer.async_stop()
    #cv2.waitKey(0)
    #cv2.destroyWindow("now")"""
    
    #record_video(False, 640, 480, 30, 3)

    #create_pictures(image_names='test', grayscale=True, resize=True, time_between_screenshots_seconds=1,
                                                #amount_of_pictures=5, camera_number=0, resize_x=640, resize_y=480)
'''




