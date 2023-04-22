import sys

import cap as cap
import cv2
import time
import threading
import requests

class TimerError(Exception):
    """A custom exception used to report errors in use of Timer class"""


class Timer:

    def __init__(self):
        self._start_time = None
        self.timer_thread = threading.Thread(target=self.start())

    def start(self):
        """Start a new timer"""
        if self._start_time is not None:
            raise TimerError("Timer is running. Use .stop() to stop it")

        self._start_time = time.perf_counter()

    def stop(self):
        """Stop the timer, and report the elapsed time"""
        if self._start_time is None:
            raise TimerError("Timer is not running. Use .start() to start it")

        elapsed_time = time.perf_counter() - self._start_time
        self._start_time = None
        print("Elapsed time: {elapsed_time:0.4f} seconds")

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


def record_video(path_to_vid, is_grayscale: bool, resolution_x: int, resolution_y: int, framerate, duration_sec, url_stop_callback_api: str, camera_id):

    capture = cv2.VideoCapture(0 + cv2.CAP_DSHOW)

    fourcc = cv2.VideoWriter_fourcc(*'XVID')
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
    r = requests.get(url_stop_callback_api, verify=False)
    return "ok"


if __name__ == "__main__":

    record_video(sys.argv[1], bool(sys.argv[2]), int(sys.argv[3]), int(sys.argv[4]), int(sys.argv[5]), int(sys.argv[6]), sys.argv[7], int(sys.argv[8]))