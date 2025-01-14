# Socialno omrežje za izposojo vozil (Vehicle Rental App)

## Kratek opis sistema
Projekt predstavlja platformo za izposojo vozil, ki deluje kot socialno omrežje, namenjeno povezovanju lastnikov vozil in najemnikov. Spletna aplikacija omogoča uporabnikom, da objavijo svoja vozila za najem, hkrati pa uporabnikom, ki potrebujejo vozilo, omogoča iskanje ustreznih vozil ter komunikacijo z lastniki za dogovor o najemu.

Uporabniki se lahko registrirajo na platformi, ustvarijo svoj profil, kjer bodo navedeni kontaktni podatki, ocene drugih uporabnikov in druge pomembne informacije. Vozila, ki so na voljo za najem, se lahko iščejo preko različnih filtrov, kot so znamka, model, letnik, cena na dan in razpoložljivost. Celoten proces najema vozila je omogočen preko platforme, od začetnega iskanja, komunikacije, pa vse do zaključka najema.

Naša rešitev omogoča fleksibilno, cenovno dostopno in učinkovito izposojo vozil ter spodbuja bolj trajnosten način mobilnosti. Sistem podpira tudi ocene in povratne informacije, ki omogočajo večjo transparentnost in zaupanja vredne interakcije med uporabniki.

## Zasloni aplikacije

### 1. Zaslon - Glavna stran
Ta zaslon deluje kot "Homepage" spletne aplikacije za uporabnike. Na spodnji strani zaslona je začetni gumb, s katerim se novi uporabniki lahko registrirajo.

![Glavna stran](https://github.com/user-attachments/assets/04b58794-f3b7-4aa6-82fa-c7a044a14f8e)

### 2. Zaslon - Seznam vozil
Na tem zaslonu si uporabniki lahko ogledajo seznam vozil, ki so na voljo za najem. Vsak vnos vsebuje osnovne informacije o vozilu, kot so znamka, model, letnik, cena na dan in povprečna ocena. Uporabniki lahko filtrirajo vozila glede na svoje preference po znamki ali po ceni.

### 3. Zaslon - Podrobnosti vozila
Ko uporabnik klikne na posamezno vozilo, se prikaže zaslon z več podrobnostmi o tem vozilu, vključno s sliko vozila, natančnimi informacijami o registrski številki, ceni in razpoložljivosti. Prav tako se tukaj spodaj uporabniki lahko povežejo z lastnikom vozila za dogovor o najemu. To storijo s tem da vtipkajo podatke o njihovem spletnem naslovu, (opcijsko) telefonsko številko in datumu začetka in konca izposoje vozila. Zraven je še mesto za sporočilo, ki ga želi poslati lastniku vozila.

Poleg tega je še zraven kalkulator, ki izračuna ceno izposoje za poljubno število dni.

### 4. Zaslon - Moja vozila
Uporabniki lahko vidijo seznam svojih vozil, za v najem. Pod vsakem vozilom so trije gumbi, ki so za spreminjanje vozila, zapis najema, in izbrisanje vozila.


### 5. Zaslon - Spreminjanje podatkov vozila
Lastnik vozila lahko po kliku na gumb "Edit" spremeni sliko, znamko, model vozila, letnico, registrsko številko, ceno najema za 1 dan in stanje razpoložljivosti.


### 6. Zaslon - Najem vozila
Lastnik vozila lahko po kliku na gumb "Rental" doda podatke o osebi, ki ima njegovo vozilo v najemu. Dodati mora ime in priimek ter številko najemnika, pa začetni in končni datum časa najema.


### 7. Zaslon - Moji najemi
Na zgornji orodni vrstici je gumb "My Rents", kjer imajo lastniki vozil izpisano zgodovino najema vseh njihovih vozil. To jim omogoča enostaven pregled. Zraven je še viden status, ki pove ali je vozilo trenutno najeto ali ne. Poleg tega je še gumb, kjer si lahko neposredni natisnejo podatke s tiskalnikom.


### 8. Zaslon - Register/Login
Novi uporabniki, ki nimajo profila, si ga lahko ustvarijo z spletnim naslovom in geslom z gumbom "Register" na desni strani. Obstoječi uporabniki se lahko enostavno prijavijo v njihov profil z gumbom "Login". Če uporabnik slučajno pozabi geslo, si ga lahko spremeni z gumbom "Forgot your password?". Poleg tega je na zaslonu za prijavo opcija za registriranje kot novi uporabnik, zraven je pa še gumb, kjer lahko uporabnik ponovno prejme pošto za potrditev spletnega naslova. Možno je tudi ostati prijavljen s potrditvijo mesta "Remember me".

Login:
![Login](https://github.com/user-attachments/assets/c9960502-2d7e-4692-a759-ea70e519cd0e)

### 9. Zaslon - Dodaj vozilo
Zaslon za dodajanje novih vozil je zelo podoben zaslonu za spreminjanje vozil. Nahaja se na desni stran zgornje orodne vrstice. Vsi podatki razen slike so nujni, drugače vozila ni mogoče dodati. Napisati je treba znamko, model vozila, letnico, registrsko številko, ceno najema za 1 dan in stanje razpoložljivosti.

![Dodaj vozilo](https://github.com/user-attachments/assets/4e7d0b36-33ca-44b8-a90a-c8095c9fa432)

### 10. Zaslon - Moja sporočila
Na zaslonu "My messages" lahko uporabnik vidi vsa sporočila, ki so mu jih poslali drugi uporabniki glede najema njegovega vozila. Poleg sporočila je zraven priložen podatek o spletnemu naslovu in pa (opcijski) telefonski številki uporabnika, da lahko nadaljujeta pogovor.

## Swagger UI

API Swagger je .NET spletna storitev ki uporablja POST in GET za sprejemanje in pošiljanje podatkov. Storitev podpira operacije CRUD in avtentikacijo/avtorizacijo zahtevkov. Spodaj so tabele.

![swagger](https://github.com/user-attachments/assets/e28a908d-21a9-4ea7-9dcd-2d79eb2c1bf0)


## Mobilna aplikacija

### 1. Zaslon - Seznam vozil
Podobno kot na spletni aplikaciji, je tudi na mobilni seznam vseh vozil.

![Available vehicles](https://github.com/user-attachments/assets/9d867743-1884-4240-8466-d4337dc3044c)

### 2. Zaslon - Podatki o vozilu
Podatki o vozilu so prikazani pod sliko vozila.

![Vozilo](https://github.com/user-attachments/assets/02830f4b-8503-49ba-92b1-d30496ce808d)


## Diagram podatkovnega modela

![Diagram](https://github.com/user-attachments/assets/71f0a68c-57f9-4196-b42b-656eb59bc227)

**Opis modela:**

AspNet tabele se uporabljajo za namene avtentikacije in managementa uporabnikov.
Druge tabele:
- **Ratings:** Predstavlja oceno, ki jo je uporabnik dal vozilu.
- **Vehicles:** Zajema podatke o posameznem vozilu.
- **Rentals:** Beleži informacije o posameznem najemu: datum, trajanje najema, ime in številka najemnika ter status.
- **Messages:** Beleži podatke o vseh sporočilih med uporabniki.

## Avtorji
- Avtor 1: [Egzon Haziri] 63220096: Podoba in funkcionalnost spletne in mobilne aplikacije
- Avtor 2: [Luka Marinko] 63230204: Poročilo
