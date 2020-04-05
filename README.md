# Hej ho :)

Czysto teoretycznie, do dodawania piosenek nie potrzeba nic prócz edytora tekstowego, wystarczy trzymać się schematu z pliku main\template.tex i dodawać każdą piosenkę w osobnym pliku.
Struktura na tę chwilę jest oparta na zespołach, więc jeżeli chcemy dodać zespół którego nie ma, tworzymy folder o możliwie krótkiej nazwie, używając _ zamiast spacji.
W folderze tworzymy plik master.tex o strukturze:

\chapter{Nazwa zespołu}
    \input{ścieżka_do_pliku}

Podstawowa struktura zakłada alfabetyczną kolejność piosenek.
Cała struktura śpiewnika znajduje się w pliku main.tex, jego nie tykamy wcale. Jedyną potencjalną potrzebą będzie dodanie nowego mastera.

Założeniem projektu jest stworzenie bazy tekstów, którą potencjalnie wykorzystać może każdy wg własnych potrzeb, tworząc swoją własną wersję pliku master.tex, czy to modyfikując style czy samą zawartość śpiewnika.
Dopisywanie piosenek jest proste i nie trzeba znać się tutaj na żadnym programowaniu. Z przetworzeniem dodanych rzeczy już jest trochę więcej zabawy.

# Składanie śpiewnika:
Najprościej używać edytora z prawdziwego zdarzenia (nie notatnika :) ) do tworzenia projektu, zdecydowanie ułatwia to pracę. Ja korzystam z InteliJ IDEA, natomiast jest to kwestią preferencji.
Pełna instrukcja instalacji krok po kroku od samego początku znajduje się tutaj:
https://github.com/Ruben-Sten/TeXiFy-IDEA#installation-instructions
Idąc za tymi instrukcjami będziecie już gotowi do pracy nad projektem :)

potrzebujemy co najmniej instalacji LaTeXa, którego można pobrać tu: https://www.latex-project.org/get/
Pod Windowsem ja korzystam z MiKTeX i najlepiej będzie się w tym względzie unifikować, różne implementacje mogą mieć swoje smaczki.
Instalacja sprowadza sie do klikania "dalej" :)
W MiKTeXie jest też dostępny podstawowy edytor LaTeXa, ale zwykły notatnik wystarczy, polecam natomiast Notepad++ jako prostą alternatywę.
Jeżeli już mamy zainstalowanego LaTeXa, wystarczy otworzyć cmd i wpisać pdflatex (ścieżka do pliku)\Spiewnik_xxx.tex, zamiast xxx podając wybraną wersję śpiewnika.
Rzecz jasna najpierw trzeba ten projekt gdzieś na dysk zassać :)

Do współtworzenia projektu potrzebny nam będzie git, dostępny do pobrania tu: https://git-scm.com/downloads
konto na GitHubie i zgłoszenie się do mnie z prośbą o współtworzenie.

Poczytajcie też trochę o gicie, podczas rejestracji na githubie wyskoczy Wam instrukcja z podstawowymi informacjami, które w zupełności tutaj wystarczą.
Wielka prośba o przemyślenie 2 razy, czy na pewno chcecie pushować do mastera. Najlepiej robić to jednak do jakiegoś brancha ;)

Przy każdym commicie starajcie się opisywać zmiany zwięźle, ale i konkretnie.
Powodzenia!
