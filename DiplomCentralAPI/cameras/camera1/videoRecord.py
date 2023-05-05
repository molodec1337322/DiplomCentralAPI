import sys

import cv2


def record_video(path_to_vid, is_grayscale: bool, resolution_x: int, resolution_y: int, framerate, duration_sec, camera_id):

    capture = cv2.VideoCapture(0 + cv2.CAP_DSHOW)

    fourcc = cv2.VideoWriter_fourcc(*'XVID')
    videoWriter = cv2.VideoWriter(path_to_vid, fourcc, framerate, (resolution_x, resolution_y))

    frames_count = framerate * duration_sec
    frames_counter = 0

    while frames_counter < frames_count:
        ret, frame = capture.read()
        if ret:
            videoWriter.write(frame)
            frames_counter += 1

        if cv2.waitKey(1) == 27:
            break

    capture.release()
    videoWriter.release()
    return "ok"



if __name__ == "__main__":

    record_video(sys.argv[1], bool(sys.argv[2]), int(sys.argv[3]), int(sys.argv[4]), int(sys.argv[5]), int(sys.argv[6]), int(sys.argv[7]))