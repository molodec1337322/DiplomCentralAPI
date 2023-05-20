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
import os


"""
video_path=sys.argv[0]  # нулевой аргумент - путь до видео - str
S_path=sys.argv[1]  # первый аргумент - путь до будущего файла с полной площадью - str
cadr=int(sys.argv[2])  # второй аргумент - номер кадра, который анализируем - int
# раскомментить когда будешь запускать через командную строку
"""

video_path = sys.argv[1] # нулевой аргумент - путь до видео - str
result_save_path = sys.argv[2]
center_path = os.path.join(os.curdir, 'analyzators', 'Full_analyzator', 'center')
min_path = os.path.join(os.curdir, 'analyzators', 'Full_analyzator', 'min')
max_path = os.path.join(os.curdir, 'analyzators', 'Full_analyzator', 'max')
full_S_path = os.path.join(os.curdir, 'analyzators', 'Full_analyzator', 'result')


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

'''
grayscale = upload_video('C:\\labs\\7sem\\diploma\\results_decoding\\classic_lines_algo\\2022-05-27 17-03-46 (online-video-cutter.com).mp4')[0]  # закомментить когда будешь запускать через командную строку
center_path = 'C:\\labs\\7sem\diploma\\results_decoding\\classic_lines_algo\\venv\\center'# закомменть когда будешь инетгрировать!!
'''

file1 = open(center_path, "r")
a = file1.read().split(',')
file1.close()

global center_x
center_x = int(a[0])
global center_y
center_y = int(a[1])

file1 = open(min_path, "r")
a = file1.read().split(',')
file1.close()

global min_x
min_x = int(a[0])
global min_y
min_y = int(a[1])

file1 = open(max_path, "r")
a = file1.read().split(',')
file1.close()

global max_x
max_x = int(a[0])
global max_y
max_y = int(a[1])

grayscale = upload_video(vide_opath)[150]   # раскомментить когда будешь запускать через командную строку

amount = 0
num_rows, num_cols = grayscale.shape
for i in range(num_rows):
    for j in range(num_cols):
        if grayscale[i][j] > 0.09:
            grayscale[i][j] = 1
            amount = amount + 1
        else:
            grayscale[i][j] = 0

'''
# принт абсолютное чилсло пикселей,процент от картинки
with open("result", mode = "w") as f:  # закомментить когда будешь запускать через командную строку
    f.write(str(amount) + "," + str(((amount/((max_y - min_y) * (max_x - min_x)))*100)))  # закомментить когда будешь запускать через командную строку
'''

with open(result_save_path, mode = "w") as f:  # раскомментить когда будешь запускать через командную строку
    f.write(str(amount) + "\n" + str(((amount/(num_rows*num_cols))*100)))  # раскомментить когда будешь запускать через командную строку
