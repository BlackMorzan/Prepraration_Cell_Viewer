# Prepraration_Cell_Viewer

Aplikacja jest stworzona w środowisku Unity w języku C#. Jej podstawowym zadaniem jest wizualizacja maipulatorów i ich środowiska aby umożliwić testy różnych metod staerowania. 

## Wizualizacja

Na daną chwilę najbardziej rozbudowaną częścią aplikacji jest część dotycząca wizualizacji. Posiada kamerę, która działa w kilku trybach. Pozawala przesuwać (pan), zbliżać, oddalać (zoom) i obracać kamerę, ale możliwe też jest zablokowanie widok na obiekcie i wszystkie powyższe metody dostosowują się do tego trybu. Skrypt opiera się o darmowy asset [RTS Camera](https://assetstore.unity.com/packages/tools/camera/rts-camera-43321), ale został znacznie zmodyfikowany. 

## Wykrywanie Kolizji

Działa tez wykrywanie kolizji między robotem, innymi elementami sceny i między przegubami. Jesli takie zdarzenie wystąpni obiekty zmieniają kolor na czerwony i lekko przeźroczysty. Kiedy obiekty przestają się dotykać wracają do swoich startowych kolorów. 

## Kinematyka prosta

Aby rozpocząć pracę nad kinemtyką odwrotną potrzebny był system sterowania osiami manipulatorów. Aby to umożliwić storzono suwaki, którym można szybko określić maksymalne kąty wychylenia i przypiąć do odpowiednich osi. Pozwala to szybko stworzyć nowe suwaki i dostosować je do maniupulatora.

## Kinemtyka odwrotna

Na razie wykonano testy różnych assetów, ale w swojej pracy inżynierskiej chciałbym porównać narzędzia dostępne w Unity z moimi badaniami.

## Dodawanie robotów

Dodawanie robotów to najbardziej czasochłonny i najmniej zautomatyzowany system i jest dokładnie opisany w [tym pliku](https://github.com/BlackMorzan/Prepraration_Cell_Viewer/blob/master/PrepCellViewer/Assets/README.txt).

## ScreenShoty


![SS1](https://github.com/BlackMorzan/Prepraration_Cell_Viewer/blob/master/PrepCellViewer/SS_PCV/PCV_SS_3.png)
![SS2](https://github.com/BlackMorzan/Prepraration_Cell_Viewer/blob/master/PrepCellViewer/SS_PCV/PCV_SS_1.png)
![SS3](https://github.com/BlackMorzan/Prepraration_Cell_Viewer/blob/master/PrepCellViewer/SS_PCV/PCV_SS_2.png)
