---

## Features

- 🔐 **Authentication & Authorization**
  - Login / Register dengan JWT
  - Role: `Admin` dan `Member`
- 📚 **Book Management**
  - Admin: Tambah, ubah, hapus buku
  - Field: Title, Author, TotalCopies, AvailableCopies
- 📖 **Loan Management**
  - Member: Pinjam dan kembalikan buku
  - Tracking status pinjaman per user
- 🧱 **Architecture**
  - Backend: Entities, DTOs, Repository, Service, Controller
  - Frontend: React + KendoReact (Grid, Form, Dialog)
- 💾 **Database**
  - SQLite via Entity Framework Core
  - Seeding: admin default + beberapa buku contoh

---

## Tech Stack

- **Backend**
  - ASP.NET Core Web API (.NET 8)
  - Entity Framework Core (SQLite)
  - JWT Authentication
  - CORS untuk React frontend

- **Frontend**
  - React + Vite
  - KendoReact (Grid, Inputs, Buttons, Dialogs)
  - Axios untuk HTTP client

## Project Structure

LibraryManagement/
├─ LibraryManagementAPI/ # Backend .NET
│ ├─ Controllers/
│ ├─ Data/
│ ├─ Helpers/
│ ├─ Models/
│ ├─ Repositories/
│ ├─ Services/
│ └─ Program.cs
└─ library-kendo-react/ # Frontend React + Kendo
├─ src/
│ ├─ api/
│ ├─ components/
│ ├─ utils/
│ ├─ App.jsx
│ └─ main.jsx
└─ vite.config.js
---

## Getting Started

### 1. Clone Repo

git clone https://github.com/ndicz/LibraryManagement.git
cd LibraryManagement

---

### 2. Backend Setup (LibraryManagementAPI)

Masuk ke folder backend:

cd LibraryManagementAPI
dotnet restore

Update database & jalankan:

dotnet ef database update
dotnet run

Backend default:

http://localhost:5293

Admin default (seeding):

username: admin
password: Admin123!

Swagger tersedia di:

http://localhost:5293/swagger

---

### 3. Frontend Setup (library-kendo-react)

Di root repo:

cd ../library-kendo-react
npm install

Pastikan `src/api/httpClient.js` mengarah ke backend yang benar:

const http = axios.create({
baseURL: "http://localhost:5293/api",
});

Jalankan frontend:

npm run dev

Frontend default:

http://localhost:5173

---

## CORS Configuration (Ringkas)

Di `LibraryManagementAPI/Program.cs` sudah diatur policy CORS agar menerima request dari React:

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
{
policy
.WithOrigins("http://localhost:5173")
.AllowAnyHeader()
.AllowAnyMethod();
});
});

...

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

---

## API Endpoints (Ringkas)

**Auth**

- `POST /api/Auth/register`
- `POST /api/Auth/login`
- `GET /api/Auth/me`

**Books**

- `GET /api/Books`
- `GET /api/Books/{id}`
- `POST /api/Books` *(Admin)*
- `PUT /api/Books/{id}` *(Admin)*
- `DELETE /api/Books/{id}` *(Admin)*

**Loans**

- `POST /api/Loans` – pinjam buku (`{ "bookId": 1 }`)
- `POST /api/Loans/{id}/return` – kembalikan buku
- `GET /api/Loans/me` – daftar pinjaman user login
- `GET /api/Loans` *(Admin)* – semua pinjaman

---

## How to Use

1. Jalankan backend (`dotnet run`) dan pastikan Swagger bisa diakses.
2. Jalankan frontend (`npm run dev`) dari folder `library-kendo-react`.
3. Buka `http://localhost:5173`.
4. Login dengan akun admin:
   - `admin` / `Admin123!`
5. Coba:
   - Lihat daftar buku di grid.
   - Tambah / edit / hapus buku (Admin).
   - Login sebagai member, pinjam & kembalikan buku.

---

## Future Improvements

- Pagination & filtering di Grid buku
- Manajemen user dari UI admin
- Pencarian buku & kategori
- Deploy ke cloud (API ke Azure / Railway, frontend ke Vercel / Netlify)

---

## License

Gunakan bebas untuk belajar dan pengembangan internal.  
Tambahkan lisensi (MIT / Apache‑2.0) jika repo akan dipublikasikan.
