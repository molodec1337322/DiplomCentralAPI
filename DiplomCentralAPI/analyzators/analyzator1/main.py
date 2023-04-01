import av
import math
import numpy as np
import skimage
import matplotlib.pyplot as plt
from skimage.color import rgb2gray
import PIL


def upload_video(path: str):
    array_of_images = []
    container = av.open(path)
    stream = container.streams.video[0]
    for packet in container.decode(stream):
        arr = rgb2gray(np.asarray(packet.to_image()))  # numpy array
        """num_rows, num_cols = arr.shape
        for i in range(num_rows):
            for j in range(num_cols):
                if arr[i][j] > 0.09:
                    arr[i][j] = 1
                else:
                    arr[i][j] = 0"""
        array_of_images.append(arr)
    return array_of_images


"""grayscale = upload_video('2022-05-27 17-03-46 (online-video-cutter.com).mp4')[0]

num_rows, num_cols = grayscale.shape
for i in range(num_rows):
    for j in range(num_cols):
        if grayscale[i][j] > 0.09:
            grayscale[i][j] = 1
        else:
            grayscale[i][j] = 0

fig, axes = plt.subplots(1, 2, figsize=(8, 4))
ax = axes.ravel()

ax[0].imshow(grayscale, cmap=plt.cm.gray)
ax[0].set_title("Grayscale")

fig.tight_layout()
plt.show()"""


def find_min_x_and_min_y(image: np.ndarray):
    min_y = 10000000
    min_x = 10000000
    num_rows, num_cols = image.shape
    for i in range(num_rows):
        for j in range(num_cols):
            if image[i][j] == 1 and min_x > i:
                min_x = i
            if image[i][j] == 1 and min_y > j:
                min_y = j
    return min_x, min_y


"""grayscale = upload_video('2022-05-27 17-03-46 (online-video-cutter.com).mp4')[0]
num_rows, num_cols = grayscale.shape
for i in range(num_rows):
    for j in range(num_cols):
        if grayscale[i][j] > 0.09:
            grayscale[i][j] = 1
        else:
            grayscale[i][j] = 0

print(find_min_x_and_min_y(grayscale))"""


def find_max_x_and_max_y(image: np.ndarray):
    max_y = -1
    max_x = -1
    num_rows, num_cols = image.shape
    for i in range(num_rows):
        for j in range(num_cols):
            if image[i][j] == 1 and max_x < i:
                max_x = i
            if image[i][j] == 1 and max_y < j:
                max_y = j
    return max_x, max_y


"""grayscale = upload_video('2022-05-27 17-03-46 (online-video-cutter.com).mp4')[0]
num_rows, num_cols = grayscale.shape
for i in range(num_rows):
    for j in range(num_cols):
        if grayscale[i][j] > 0.09:
            grayscale[i][j] = 1
        else:
            grayscale[i][j] = 0

print(find_max_x_and_max_y(grayscale))"""


def find_parameter_trash_holds(image: np.ndarray, number_of_parameter: int):
    min_x, min_y = find_min_x_and_min_y(image)
    max_x, max_y = find_max_x_and_max_y(image)
    center_x, center_y = math.floor((min_x + max_x) / 2), math.floor((min_y + max_y) / 2)
    lower_trash_hold_x = center_x
    middle_trash_hold_x = center_x
    lower_trash_hold_y = center_y
    middle_trash_hold_y = center_y

    distance_x = math.floor((max_x - center_x) / 4)
    distance_y = math.floor((max_y - center_y) / 4)

    if number_of_parameter == 1:
        lower_trash_hold_x = center_x - distance_x * 2
        middle_trash_hold_x = center_x - distance_x * 3
    elif number_of_parameter == 2:
        lower_trash_hold_x = center_x - distance_x * 2
        lower_trash_hold_y = center_y + distance_y * 2
        middle_trash_hold_x = center_x - distance_x * 3
        middle_trash_hold_y = center_y + distance_y * 3
    elif number_of_parameter == 3:
        lower_trash_hold_y = center_y + distance_y * 2
        middle_trash_hold_y = center_y + distance_y * 3
    elif number_of_parameter == 4:
        lower_trash_hold_x = center_x + distance_x * 2
        lower_trash_hold_y = center_y + distance_y * 2
        middle_trash_hold_x = center_x + distance_x * 3
        middle_trash_hold_y = center_y + distance_y * 3
    elif number_of_parameter == 5:
        lower_trash_hold_x = center_x + distance_x * 2
        middle_trash_hold_x = center_x + distance_x * 3
    elif number_of_parameter == 6:
        lower_trash_hold_x = center_x + distance_x * 2
        lower_trash_hold_y = center_y - distance_y * 2
        middle_trash_hold_x = center_x + distance_x * 3
        middle_trash_hold_y = center_y - distance_y * 3
    elif number_of_parameter == 7:
        lower_trash_hold_y = center_y - distance_y * 2
        middle_trash_hold_y = center_y - distance_y * 3
    elif number_of_parameter == 8:
        lower_trash_hold_x = center_x - distance_x * 2
        lower_trash_hold_y = center_y - distance_y * 2
        middle_trash_hold_x = center_x - distance_x * 3
        middle_trash_hold_y = center_y - distance_y * 3

    return (lower_trash_hold_x, lower_trash_hold_y), (middle_trash_hold_x, middle_trash_hold_y)


"""grayscale = upload_video('2022-05-27 17-03-46 (online-video-cutter.com).mp4')[0]
num_rows, num_cols = grayscale.shape
for i in range(num_rows):
    for j in range(num_cols):
        if grayscale[i][j] > 0.09:
            grayscale[i][j] = 1
        else:
            grayscale[i][j] = 0

for i in range(1, 9):
    print(find_parameter_trash_holds(grayscale, i))"""


def if_diag_black(image, number_of_parameter, parameter_trash_hold_x, parameter_trash_hold_y):
    if number_of_parameter == 2:
        if (image[parameter_trash_hold_x - 2][parameter_trash_hold_y + 2] == 0 and
        image[parameter_trash_hold_x - 1][parameter_trash_hold_y + 2] == 0 and
        image[parameter_trash_hold_x - 2][parameter_trash_hold_y + 1] == 0 and
        image[parameter_trash_hold_x - 2][parameter_trash_hold_y] == 0 and
        image[parameter_trash_hold_x - 1][parameter_trash_hold_y + 1] == 0 and
        image[parameter_trash_hold_x][parameter_trash_hold_y + 2] == 0 and
        image[parameter_trash_hold_x - 2][parameter_trash_hold_y - 1] == 0 and
        image[parameter_trash_hold_x - 1][parameter_trash_hold_y] == 0 and
        image[parameter_trash_hold_x][parameter_trash_hold_y + 1] == 0 and
        image[parameter_trash_hold_x + 1][parameter_trash_hold_y + 2] == 0 and
        image[parameter_trash_hold_x - 2][parameter_trash_hold_y - 2] == 0 and
        image[parameter_trash_hold_x - 1][parameter_trash_hold_y - 1] == 0 and
        image[parameter_trash_hold_x][parameter_trash_hold_y] == 0 and
        image[parameter_trash_hold_x + 1][parameter_trash_hold_y + 1] == 0 and
        image[parameter_trash_hold_x + 2][parameter_trash_hold_y + 2] == 0):
            return True
        else:
            return False
    elif number_of_parameter == 4:
        if (image[parameter_trash_hold_x - 2][parameter_trash_hold_y + 2] == 0 and
        image[parameter_trash_hold_x - 1][parameter_trash_hold_y + 2] == 0 and
        image[parameter_trash_hold_x + 1][parameter_trash_hold_y - 1] == 0 and
        image[parameter_trash_hold_x + 2][parameter_trash_hold_y - 2] == 0 and
        image[parameter_trash_hold_x - 1][parameter_trash_hold_y + 1] == 0 and
        image[parameter_trash_hold_x][parameter_trash_hold_y + 2] == 0 and
        image[parameter_trash_hold_x + 2][parameter_trash_hold_y - 1] == 0 and
        image[parameter_trash_hold_x + 1][parameter_trash_hold_y] == 0 and
        image[parameter_trash_hold_x][parameter_trash_hold_y + 1] == 0 and
        image[parameter_trash_hold_x + 1][parameter_trash_hold_y + 2] == 0 and
        image[parameter_trash_hold_x + 2][parameter_trash_hold_y] == 0 and
        image[parameter_trash_hold_x + 2][parameter_trash_hold_y + 1] == 0 and
        image[parameter_trash_hold_x][parameter_trash_hold_y] == 0 and
        image[parameter_trash_hold_x + 1][parameter_trash_hold_y + 1] == 0 and
        image[parameter_trash_hold_x + 2][parameter_trash_hold_y + 2] == 0):
            return True
        else:
            return False
    elif number_of_parameter == 6:
        if (image[parameter_trash_hold_x + 2][parameter_trash_hold_y - 2] == 0 and
        image[parameter_trash_hold_x + 1][parameter_trash_hold_y - 2] == 0 and
        image[parameter_trash_hold_x + 2][parameter_trash_hold_y - 1] == 0 and
        image[parameter_trash_hold_x + 2][parameter_trash_hold_y] == 0 and
        image[parameter_trash_hold_x + 1][parameter_trash_hold_y - 1] == 0 and
        image[parameter_trash_hold_x][parameter_trash_hold_y - 2] == 0 and
        image[parameter_trash_hold_x + 2][parameter_trash_hold_y + 1] == 0 and
        image[parameter_trash_hold_x + 1][parameter_trash_hold_y] == 0 and
        image[parameter_trash_hold_x][parameter_trash_hold_y - 1] == 0 and
        image[parameter_trash_hold_x - 1][parameter_trash_hold_y - 2] == 0 and
        image[parameter_trash_hold_x + 2][parameter_trash_hold_y + 2] == 0 and
        image[parameter_trash_hold_x + 1][parameter_trash_hold_y + 1] == 0 and
        image[parameter_trash_hold_x][parameter_trash_hold_y] == 0 and
        image[parameter_trash_hold_x - 1][parameter_trash_hold_y - 1] == 0 and
        image[parameter_trash_hold_x - 2][parameter_trash_hold_y - 2] == 0):
            return True
        else:
            return False
    elif number_of_parameter == 8:
        if (image[parameter_trash_hold_x + 2][parameter_trash_hold_y - 2] == 0 and
        image[parameter_trash_hold_x + 1][parameter_trash_hold_y - 2] == 0 and
        image[parameter_trash_hold_x - 1][parameter_trash_hold_y + 1] == 0 and
        image[parameter_trash_hold_x - 2][parameter_trash_hold_y + 2] == 0 and
        image[parameter_trash_hold_x + 1][parameter_trash_hold_y - 1] == 0 and
        image[parameter_trash_hold_x][parameter_trash_hold_y - 2] == 0 and
        image[parameter_trash_hold_x - 2][parameter_trash_hold_y + 1] == 0 and
        image[parameter_trash_hold_x - 1][parameter_trash_hold_y] == 0 and
        image[parameter_trash_hold_x][parameter_trash_hold_y - 1] == 0 and
        image[parameter_trash_hold_x - 1][parameter_trash_hold_y - 2] == 0 and
        image[parameter_trash_hold_x - 2][parameter_trash_hold_y] == 0 and
        image[parameter_trash_hold_x - 2][parameter_trash_hold_y - 1] == 0 and
        image[parameter_trash_hold_x][parameter_trash_hold_y] == 0 and
        image[parameter_trash_hold_x - 1][parameter_trash_hold_y - 1] == 0 and
        image[parameter_trash_hold_x - 2][parameter_trash_hold_y - 2] == 0):
            return True
        else:
            return False
    else:
        return False


def find_parameters_values(image):
    parameter_value_2 = -1
    parameter_value_4 = -1
    parameter_value_6 = -1
    parameter_value_8 = -1

    if (if_diag_black(image, 2, find_parameter_trash_holds(image, 2)[0][0], find_parameter_trash_holds(image, 2)[0][1])
        and if_diag_black(image, 2, find_parameter_trash_holds(image, 2)[1][0], find_parameter_trash_holds(image, 2)[1][1])):
        parameter_value_2 = 0
    elif (not if_diag_black(image, 2, find_parameter_trash_holds(image, 2)[0][0], find_parameter_trash_holds(image, 2)[0][1])
        and if_diag_black(image, 2, find_parameter_trash_holds(image, 2)[1][0], find_parameter_trash_holds(image, 2)[1][1])):
        parameter_value_2 = 1
    else:
        parameter_value_2 = 2

    if (if_diag_black(image, 4, find_parameter_trash_holds(image, 4)[0][0], find_parameter_trash_holds(image, 4)[0][1])
        and if_diag_black(image, 4, find_parameter_trash_holds(image, 4)[1][0], find_parameter_trash_holds(image, 4)[1][1])):
        parameter_value_4 = 0
    elif (not if_diag_black(image, 4, find_parameter_trash_holds(image, 4)[0][0], find_parameter_trash_holds(image, 4)[0][1])
        and if_diag_black(image, 4, find_parameter_trash_holds(image, 4)[1][0], find_parameter_trash_holds(image, 4)[1][1])):
        parameter_value_4 = 1
    else:
        parameter_value_4 = 2

    if (if_diag_black(image, 6, find_parameter_trash_holds(image, 6)[0][0], find_parameter_trash_holds(image, 6)[0][1])
        and if_diag_black(image, 6, find_parameter_trash_holds(image, 6)[1][0], find_parameter_trash_holds(image, 6)[1][1])):
        parameter_value_6 = 0
    elif (not if_diag_black(image, 6, find_parameter_trash_holds(image, 6)[0][0], find_parameter_trash_holds(image, 6)[0][1])
        and if_diag_black(image, 6, find_parameter_trash_holds(image, 6)[1][0], find_parameter_trash_holds(image, 6)[1][1])):
        parameter_value_6 = 1
    else:
        parameter_value_6 = 2

    if (if_diag_black(image, 8, find_parameter_trash_holds(image, 8)[0][0], find_parameter_trash_holds(image, 8)[0][1])
        and if_diag_black(image, 8, find_parameter_trash_holds(image, 8)[1][0], find_parameter_trash_holds(image, 8)[1][1])):
        parameter_value_8 = 0
    elif (not if_diag_black(image, 8, find_parameter_trash_holds(image, 8)[0][0], find_parameter_trash_holds(image, 8)[0][1])
        and if_diag_black(image, 8, find_parameter_trash_holds(image, 8)[1][0], find_parameter_trash_holds(image, 8)[1][1])):
        parameter_value_8 = 1
    else:
        parameter_value_8 = 2

    return parameter_value_2, parameter_value_4, parameter_value_6, parameter_value_8


grayscale = upload_video('-max-_min_max_max.mp4')[0]
num_rows, num_cols = grayscale.shape
for i in range(num_rows):
    for j in range(num_cols):
        if grayscale[i][j] > 0.09:
            grayscale[i][j] = 1
        else:
            grayscale[i][j] = 0

print(find_parameters_values(grayscale))
