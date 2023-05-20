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



video_path = sys.argv[1]
result_save_path = sys.argv[2]# нулевой аргумент - путь до видео - str
center_path = os.path.join(os.curdir, 'analyzators', 'Full_analyzator', 'center')
min_path = os.path.join(os.curdir, 'analyzators', 'Full_analyzator', 'min')
max_path = os.path.join(os.curdir, 'analyzators', 'Full_analyzator', 'max')
partitial_S_path = os.path.join(os.curdir, 'analyzators', 'Full_analyzator', 'result')

"""
center_path=sys.argv[1]  # первый аргумент - путь до файлы с кордами центра прямоугольника - str
min_path=sys.argv[2]  # второй аргумент - путь до будущего файла с минимум - str
max_path=sys.argv[3]  # третий аргумент - путь до будущего файла с максимумом - str
partitial_S_path=sys.argv[4]  # четвертый аргумент - путь до будущего файла с частичными площадями - str
cadr=int(sys.argv[5])  # пятый аргумент - номер кадра, который анализируем - int
# раскомментить когда будешь запускать через командную строку
"""

"""
center_path = 'C:\\labs\\7sem\diploma\\results_decoding\\classic_lines_algo\\venv\\center'# закомменть когда будешь инетгрировать!!
partitial_S_path = 'Spath'# закомменть когда будешь инетгрировать!!
min_path = 'min'# закомменть когда будешь инетгрировать!!
max_path = 'max'# закомменть когда будешь инетгрировать!!
"""

def upload_video(path: str):
    array_of_images = []
    container = av.open(path)
    stream = container.streams.video[0]
    for packet in container.decode(stream):
        arr = rgb2gray(np.asarray(packet.to_image()))  # numpy array
        num_rows, num_cols = arr.shape
        for i in range(num_rows):
            for j in range(num_cols):
                if arr[i][j] > 0.09:
                    arr[i][j] = 1
                else:
                    arr[i][j] = 0
        array_of_images.append(arr)
    return array_of_images

#grayscale = upload_video('C:\\labs\\7sem\\diploma\\results_decoding\\classic_lines_algo\\2022-05-27 17-03-46 (online-video-cutter.com).mp4')[0]  # закомментить когда будешь запускать через командную строку

grayscale = upload_video(video_path)[150]   # раскомментить когда будешь запускать через командную строку


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

#print(center_x,center_y,min_x,min_y,max_x,max_y)

def up_lv_diag(x,y):
    result = (x - center_x) * (min_y - center_y) - (y - center_y) * (min_x - center_x)
    if result >= 0:
        return True
    else:
        return False

def up_rv_diag(x,y):
    result = (x - center_x) * (max_y - center_y) - (y - center_y) * (min_x - center_x)
    if result >= 0:
        return False
    else:
        return True

S1 = 0
S2 = 0
S3 = 0
S4 = 0
S5 = 0
S6 = 0
S7 = 0
S8 = 0

for i in range(min_x, max_x):
    for j in range(min_y, max_y):
        if i < center_x:
            if j < center_y:
                if up_lv_diag(i, j):
                    S8 = S8 + 1
                else:
                    S7 = S7 + 1
            else:
                if up_rv_diag(i, j):
                    S1 = S1 + 1
                else:
                    S2 = S2 + 1
        else:
            if j >= center_y:
                if up_lv_diag(i, j):
                    S3 = S3 + 1
                else:
                    S4 = S4 + 1
            else:
                if up_rv_diag(i, j):
                    S6 = S6 + 1
                else:
                    S5 = S5 + 1

#print(S1, S2, S3, S4, S5, S6, S7, S8)
#print(str(S1) + "," + str(((S1/((max_y - min_y) * (max_x - min_x)))*100)))

with open(result_save_path, mode = "w") as f:  # закомментить когда будешь запускать через командную строку
    f.write(str(S1) + "," + str(((S1/((max_y - min_y) * (max_x - min_x)))*100)) + "\n"
            + str(S2) + "," + str(((S2/((max_y - min_y) * (max_x - min_x)))*100)) + "\n" +
            str(S3) + "," + str(((S3/((max_y - min_y) * (max_x - min_x)))*100)) + "\n" +
            str(S4) + "," + str(((S4/((max_y - min_y) * (max_x - min_x)))*100)) + "\n" +
            str(S5) + "," + str(((S5/((max_y - min_y) * (max_x - min_x)))*100)) + "\n" +
            str(S6) + "," + str(((S6/((max_y - min_y) * (max_x - min_x)))*100)) + "\n" +
            str(S7) + "," + str(((S7/((max_y - min_y) * (max_x - min_x)))*100)) + "\n" +
            str(S8) + "," + str(((S8/((max_y - min_y) * (max_x - min_x)))*100)) + "\n")  # закомментить когда будешь запускать через командную строку
    print("result")



