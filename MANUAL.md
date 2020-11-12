# Instrukcja dla Projektu Spec:

### Simulink

1. W Simulink podepnij 2 bloczki "toWorkspace" do wyścia z robota (q1, q2) (przed liczeniem błędu) 
2. Zmień "Variable name" na odpowiednio "q1" i "q2"
3. W skrypcie matlab umieść:

```matlab
Combined = [Simout.tout.'; Simout.q1.'; Simout.q2.'];

fileID = fopen('data.txt','w');
formatSpec = '%d %d %d \r\n';
fprintf(fileID, '%2.4f %2.4f %2.4f \r\n', Combined);
fclose(fileID);
```
4. Utworzony plik powinien zwierać 3 kolumny
czas q1 q2
5. Przenieś plik do folderu z programem

### PCV

1. Wybierz z dostępnych robotów EDDA (niebieska)
2. Wybierz tryb pracy na automatyczny (niebieska)
3. Wpisz nazwę pliku do pola poniżej "Load Data" (zielona)
4. Kliknij "Load Data" (zielona)
5. Jeśli przycisk jest zielony dane zostały zapisane
6. Kliknij "Play/Stop" 
7. Powinieceś zobaczyć robota wykonującego zaprogramowane ruchy.

# UI!

[S1](https://github.com/BlackMorzan/Prepraration_Cell_Viewer/blob/master/Photos/UI.png)

1. Górny lewy róg (góra) przycisk zmienia robota (niebiski(1))
2. Górny lewy róg (dół) przycisk zmienia tryb pracy robota (niebieski(1))
 a) tryb pracy manualnej (brąz(4))
 b) tryb otwarzania ścieżki (żółty(2))
 c) brak
3. Play/Pause - rozpocznij lub zarzymaj odtwarzanie
4. Stop - przerwij odtwarzanie
4. Górny prawy róg "Wyjście z programu"
5. Lewe dolne suwaki pozwalają na manualne porusznie przegubami (brąz(4))
6. Prawe dolne suwaki pozwalają na zdefiniowanie, dla jakich prędkości wizualizacja ścieżki ma zmieniać kolor (czerwień(5))


# Poruszanie Kamerą

1. Prawy przycisk + ruch myszą - obrót wokół własnej osi
2. Środkowy przycisk myszy + ruch myszą - porusznie się po płaszczyźnie X, Y
3. Lewy przycisk - zaznacza obiekt
4. Lewy alt - patrz na zaznaczony obiekt
5. Prawy przycisk + l alt + ruch myszą - obrót wokół zaznaczonego obiektu
6. Kółko myszy - poruszanie się góra/dół
7. Kółko myszy + alt - zbliżanie/oddalanie się od zaznaczonego obiektu