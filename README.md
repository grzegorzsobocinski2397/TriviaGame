# Trivia Game

Jest to projekt napisany na zaliczenie zajęć "Programowanie Obiektowe". 
Aplikacja służąca do szybkiej zabawy w grę prawda lub fałsz z dowolnymi pytaniami. Pytania można dodawać w formacie .CSV gdzie pierwszą kolumną jest pytanie a drugą odpowiedź (0 lub 1). 

Aplikacja serializuje wczytane pytania oraz szyfruje (używając AES) do ponownej szybkiej gry. 
Aplikacja serializuje wyniki osiągnięte przez gracza i wyświetla je w formie tabeli wyników. 

# Instalacja
Projekt wystarczy sklonować a następnie pobrać wymagane paczki. 

# Jak używać
Pytania należy zapisać w formacie .CSV 
Prawidłowe zdania oznaczamy cyfrą 1 natomiast fałszywe 0.

| Statement         | Answer |
|---------------------|---|
| Pigs are pink.      | 1 |
| Santa Claus exists. | 0 |

Plik powinien mieć dwie kolumny oraz dowolną ilość wierszy. Pytania wczytujemy przy pomocy przycisku Load.
Pytania są serializowane, dzięki czemu nie musimy za każdym razem wczytywać pytań. Wynik użytkownika zostaje dodany do pliku z rekordami.

# Figma Design
<a>https://www.figma.com/file/SRWnWS84i3dmvjowB2w5zw/Projekt-Semestralny?node-id=0%3A1</a>

# Autorzy

- Grzegorz Sobociński -  [grzegorzsobocinski2397](https://github.com/grzegorzsobocinski2397) 
- Piotr Szczypka - [xkertoip](https://github.com/xkertoip)
