import av
import math
import numpy as np
import skimage
import matplotlib.pyplot as plt
from skimage.color import rgb2gray
import PIL
from skimage import data, filters, color, morphology
from skimage.segmentation import flood, flood_fill
import sys
import subprocess
import os



video_path = sys.argv[1]  # нулевой аргумент - путь до видео - str
result_save_path = sys.argv[2]
center_path = os.path.join(os.curdir, 'analyzators', 'Partial_analyzator', 'center')
min_path = os.path.join(os.curdir, 'analyzators', 'Partial_analyzator', 'min')
max_path = os.path.join(os.curdir, 'analyzators', 'Partial_analyzator', 'max')
'''
center_path=sys.argv[2]  # первый аргумент - путь до будущего файла с центром - str
min_path=sys.argv[3]  # второй аргумент - путь до будущего файла с минимум - str
max_path=sys.argv[4]  # третий аргумент - путь до будущего файла с максимумом - str
# раскомментить когда будешь запускать через командную строку
'''



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


#grayscale = upload_video('C:\\labs\\7sem\\diploma\\results_decoding\\classic_lines_algo\\2022-05-27 17-03-46 (online-video-cutter.com).mp4')[0]  # закомментить когда будешь запускать через командную строку

grayscale = upload_video(video_path)[5]
# раскомментить когда будешь запускать через командную строку

num_rows, num_cols = grayscale.shape
for i in range(num_rows):
    for j in range(num_cols):
        if grayscale[i][j] > 0.09:
            grayscale[i][j] = 1
        else:
            grayscale[i][j] = 0


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

min_x, min_y = find_min_x_and_min_y(grayscale)
max_x, max_y = find_max_x_and_max_y(grayscale)
centr_x = round((min_x+max_x)/2)
centr_y = round((min_y+max_y)/2)
#print((min_x+max_x)/2, (min_y+max_y)/2)

'''
with open("center", mode = "w") as f:  # закомментить когда будешь запускать через командную строку
    f.write(str(centr_x) + "," + str(centr_y))  # закомментить когда будешь запускать через командную строку
    print("center")

with open("min", mode = "w") as f:  # закомментить когда будешь запускать через командную строку
    f.write(str(min_x) + "," + str(min_y))  # закомментить когда будешь запускать через командную строку
    print("min")

with open("max", mode = "w") as f:  # закомментить когда будешь запускать через командную строку
    f.write(str(max_x) + "," + str(max_y))  # закомментить когда будешь запускать через командную строку
    print("max")

'''
#exec(open("partial_S.py").read())


with open(center_path, mode = "w") as f:
    f.write(str(centr_x) + "," + str(centr_y))

with open(min_path, mode = "w") as f: 
    f.write(str(min_x) + "," + str(min_y)) 

with open(max_path, mode = "w") as f:  
    f.write(str(max_x) + "," + str(max_y))  
  # раскомментить когда будешь запускать через командную строку

print(os.path.join(os.curdir, 'analyzators', 'Partial_analyzator', 'partial_S.py'))
subprocess.Popen(['py.exe', os.path.join(os.curdir, 'analyzators', 'Partial_analyzator', 'partial_S.py'), video_path, result_save_path], shell=True)



