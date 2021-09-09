PhotoKinia
===
Simple application for your photos management. 

![Screenshot1](/docs/images/screenshot1.jpg)

It takes images ,videos from selected directories and place them in prepare directories structue:

* {year}
    * {month_number}
        * {day_number}

Example:
Photo myExampleImage.jpg taken 21st of May 2019 will end up at this place: .../2019/5/21/myExampleImage.jpg

![Screenshot1](/docs/images/screenshot2.jpg)

Application using EXIF information to determine creation date. If two images with the same names will endup at the same location then PhotoKinia will compare image files using MD5 hash. Depending on comparision result it will skip duplicated file, or just modify image name by adding number to it.

At this moment application supports *.jpg, *.dng and *.mp4 file formats.

