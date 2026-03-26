# 🎫 Ticket System

Prosty system do zarządzania zgłoszeniami (ticketami) zbudowany w oparciu o:

* Frontend: Next.js
* Backend: .NET + Entity Framework
* Baza danych: MySQL

---

## 🔧 Wymagania

Przed uruchomieniem upewnij się, że masz zainstalowane:

* Node.js (>= 18)
* npm lub yarn
* .NET SDK (np. .NET 7 lub 8)
* MySQL Server
* (opcjonalnie) `dotnet-ef`:

  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## 📁 Struktura projektu

```
/frontend   → aplikacja Next.js
/backend    → API w .NET
```

---

## 🚀 Uruchomienie projektu

### 1. Backend

1. Przejdź do folderu backend:

   ```bash
   cd backend
   ```

2. Utwórz plik `.env`:

   ```env
   DB_CONNECTION_STRING="Server=localhost;Port=3306;Database=ticket;User=root;Password=;"
   ```

3. Utwórz migracje i zaktualizuj bazę:

   ```bash
   dotnet ef migrations add Init
   dotnet ef database update
   ```

4. Uruchom backend:

   ```bash
   dotnet run
   ```

Backend uruchomi się na porcie widocznym w konsoli (np. `http://localhost:5229/`).

---

### 2. Frontend

1. Przejdź do folderu frontend:

   ```bash
   cd frontend
   ```

2. Utwórz plik `.env`:

   ```env
   NEXT_PUBLIC_APP_URL="http://localhost:5229/"
   ```

   ⚠️ Uwaga: port musi być taki sam jak backendu

3. Zainstaluj zależności:

   ```bash
   npm install
   ```

4. Uruchom aplikację:

   ```bash
   npm run dev
   ```

Frontend będzie dostępny pod:

```
http://localhost:3000
```

---

## 🗄️ Baza danych

* Upewnij się, że MySQL działa
* Baza `ticket`:

  * może zostać utworzona automatycznie przez migracje
  * lub możesz ją stworzyć ręcznie

---

## 🔌 Konfiguracja środowiska

### Backend `.env`

```
DB_CONNECTION_STRING="Server=localhost;Port=3306;Database=ticket;User=root;Password=;"
```

### Frontend `.env`

```
NEXT_PUBLIC_APP_URL="http://localhost:5229/"
```

---

## ⚠️ Najczęstsze problemy

### ❌ Backend nie startuje

* sprawdź czy działa MySQL
* sprawdź connection string

### ❌ Błąd połączenia z bazą

* upewnij się, że:

  * port MySQL to `3306`
  * użytkownik i hasło są poprawne

### ❌ Migracje nie działają

* upewnij się, że masz:

  ```bash
  dotnet tool install --global dotnet-ef
  ```

### ❌ Frontend nie łączy się z backendem

* sprawdź:

  * `NEXT_PUBLIC_APP_URL`
  * port backendu

---

## 💡 Dodatkowe uwagi

* Jeśli zmienisz modele w backend:

  ```bash
  dotnet ef migrations add Update
  dotnet ef database update
  ```
* Backend musi być uruchomiony przed frontendem
