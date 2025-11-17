# Atributų Pakitimų Logai / Attribute Change Logs

## 🎯 Naujas Funkcionalumas / New Feature

Po kiekvieno raundo dabar rodoma **detali lentelė** su **visais atributų pakitimais**!

## 📊 Kaip Atrodo / How It Looks 

### Raundo Santrauka su Detaliais Logais:

```
╔════════════════════════════════════════════════════════╗
║              RAUNDO SANTRAUKA / ROUND SUMMARY          ║
╚════════════════════════════════════════════════════════╝

🏙️  Vilnius

📊 Detalūs pakitimai - Vilnius:
   ┌─────────────────┬─────────┬─────────┬──────────┐
   │ Atributas       │ Buvo    │ Tapo    │ Pokytis  │
   ├─────────────────┼─────────┼─────────┼──────────┤
   │ Biudžetas       │    1500 │    1320 │   -180 ↓ │
   │ Populiacija     │    1200 │    1215 │    +15 ↑ │
   │ Laimė           │      75 │      78 │     +3 ↑ │
   │ Saugumas        │      70 │      72 │     +2 ↑ │
   │ Aplinka         │      68 │      63 │     -5 ↓ │
   │ Infrastruktūra  │       2 │       3 │     +1 ↑ │
   │ Pastatai        │       1 │       2 │     +1 ↑ │
   └─────────────────┴─────────┴─────────┴──────────┘

🏙️  Kaunas

📊 Detalūs pakitimai - Kaunas:
   ┌─────────────────┬─────────┬─────────┬──────────┐
   │ Atributas       │ Buvo    │ Tapo    │ Pokytis  │
   ├─────────────────┼─────────┼─────────┼──────────┤
   │ Biudžetas       │    1100 │    1280 │   +180 ↑ │
   │ Populiacija     │    1050 │    1050 │      0 → │
   │ Laimė           │      80 │      72 │     -8 ↓ │
   │ Saugumas        │      65 │      75 │    +10 ↑ │
   │ Aplinka         │      70 │      68 │     -2 ↓ │
   │ Infrastruktūra  │       1 │       1 │      0 → │
   │ Pastatai        │       0 │       0 │      0 → │
   └─────────────────┴─────────┴─────────┴──────────┘

────────────────────────────────────────────────────────────
=== Dabartinė būsena ===
Vilnius: Pop=1215, Budget=1320, Happy=78, Sec=72, Env=63, Infra=3, Buildings=2
Kaunas: Pop=1050, Budget=1280, Happy=72, Sec=75, Env=68, Infra=1, Buildings=0
```

## 🎨 Vizualiniai Elementai / Visual Elements

### 1. **Lentelės Struktūra / Table Structure**
- ✅ Graži ASCII lentelė su kraštinėmis
- ✅ 4 stulpeliai: Atributas, Buvo, Tapo, Pokytis
- ✅ Lygiuotos reikšmės (lengviau skaityti)

### 2. **Pokytis Indikatoriai / Change Indicators**
- ⬆️ **↑** - Padidėjo (teigiamas pokytis)
- ⬇️ **↓** - Sumažėjo (neigiamas pokytis)
- ➡️ **→** - Nepasikeitė (0 pokytis)

### 3. **Spalvos / Colors**
- 🔵 **Cyan** - Miesto pavadinimas (aktyvus)
- 🔴 **Red** - Pralaimėtas miestas
- 🟢 **Green** - Dabartinė būsena (sveiki miestai)

### 4. **Emoji Ženkliukai / Emoji Indicators**
- 🏙️ - Miestas
- 📊 - Detalūs pakitimai
- ❌ - Pralaimėtas
- ╔═╗ - Dekoratyvus rėmelis

## 📋 Rodomi Atributai / Displayed Attributes

Kiekvienam miestui rodomi **visi 7 atributai**:

1. **Biudžetas** - Pinigų kiekis
2. **Populiacija** - Gyventojų skaičius
3. **Laimė** - Gyventojų pasitenkinimas (0-100)
4. **Saugumas** - Miesto saugumas (0-100)
5. **Aplinka** - Aplinkos kokybė (0-100)
6. **Infrastruktūra** - Infrastruktūros lygis (0-10)
7. **Pastatai** - Pastatų skaičius

## 💡 Kodėl Tai Svarbu? / Why This Matters?

### Strateginis Pranašumas:
- 📈 **Analizė**: Matote tiksliai, kaip kiekvienas sprendimas paveikė miestą
- 🎯 **Planavimas**: Lengviau planuoti kitus ėjimus matant tendencijas
- 📊 **Stebėjimas**: Sekti, ar jūsų strategija veikia ilguoju laikotarpiu
- 🔍 **Detalumas**: Nėra paslėptų pakitimų - viskas matoma

### Žaidimo Patirtis:
- ✨ Profesionalesnė išvaizda
- 📖 Lengviau suprasti, kas vyksta
- 🎮 Gilesnis strateginis žaidimas
- 🏆 Geresnė grįžtamoji informacija

## 🔧 Techninė Implementacija / Technical Implementation

### Nauji Metodai City.cs:
```csharp
public string GetDetailedLog(string cityName, CitySnapshot before, CitySnapshot after)
{
    // Sukuria detalią lentelę su visais pakitimais
    // Shows table with before/after/change for all attributes
}

private void AppendAttributeLine(StringBuilder log, string name, int before, int after, int change)
{
    // Prideda vieną eilutę į lentelę su pokytį indikuojančiu simboliu
    // Adds one line to table with change indicator
}
```

### Patobulinta Game.cs:
```csharp
// Raundo pabaigoje:
var stats = city.CalculateChanges(snapshot);
var currentSnapshot = city.CreateSnapshot();

// Rodo detalų logą
Console.WriteLine(stats.GetDetailedLog(city.Name, snapshot, currentSnapshot));
```

## 📝 Pavyzdinis Scenarijus / Example Scenario

**Raundo pradžia:**
- Žaidėjas pasirenka "Statyti infrastruktūrą" → Kaina 250
- Pastatoma Gyvenamasis namas → Kaina 400, +7 laimė, +12 populiacija
- Įvykis: "Filantropų Auka" → +200 biudžetas, +10 laimė
- Raundo atnaujinimas: -80 priežiūra, -1 aplinka

**Rezultatas lentelėje:**
```
📊 Detalūs pakitimai - Vilnius:
   ┌─────────────────┬─────────┬─────────┬──────────┐
   │ Biudžetas       │    1500 │    1170 │   -330 ↓ │ (-250-400+200-80)
   │ Populiacija     │    1200 │    1212 │    +12 ↑ │ (+12 iš namo)
   │ Laimė           │      75 │      92 │    +17 ↑ │ (+7+10)
   │ Aplinka         │      68 │      65 │     -3 ↓ │ (-2 namas, -1 decay)
   │ Infrastruktūra  │       2 │       3 │     +1 ↑ │ (+1 iš veiksmo)
   │ Pastatai        │       1 │       2 │     +1 ↑ │ (+1 naujas namas)
   └─────────────────┴─────────┴─────────┴──────────┘
```

Visa matematika **aiški** ir **matoma**! 🎉

## ✅ Funkcionalumo Patikrinimas / Feature Verification

- ✅ Lentelė rodoma po kiekvieno raundo
- ✅ Visi 7 atributai visada rodomi
- ✅ Pokytis indikatoriai (↑↓→) veikia teisingai
- ✅ Spalvos pritaikytos (cyan/red/green)
- ✅ Lentelės formatavimas teisingas
- ✅ "Jokių pakitimų" pranešimas, jei nieko nepasikeitė
- ✅ Veikia su keliais miestais vienu metu
- ✅ Projektas kompiliuojasi be klaidų

## 🎮 Žaidėjo Patirtis / Player Experience

**Prieš:**
```
=== Raundo Santrauka ===
Vilnius: Biudžetas: -330, Populiacija: +12, Laimė: +17
```

**Dabar:**
```
📊 Detalūs pakitimai - Vilnius:
   ┌─────────────────┬─────────┬─────────┬──────────┐
   │ Atributas       │ Buvo    │ Tapo    │ Pokytis  │
   ├─────────────────┼─────────┼─────────┼──────────┤
   │ Biudžetas       │    1500 │    1170 │   -330 ↓ │
   │ Populiacija     │    1200 │    1212 │    +12 ↑ │
   │ Laimė           │      75 │      92 │    +17 ↑ │
   │ Saugumas        │      70 │      70 │      0 → │
   │ Aplinka         │      68 │      65 │     -3 ↓ │
   │ Infrastruktūra  │       2 │       3 │     +1 ↑ │
   │ Pastatai        │       1 │       2 │     +1 ↑ │
   └─────────────────┴─────────┴─────────┴──────────┘
```

**Daug aiškiau ir informatyviau!** 🌟
