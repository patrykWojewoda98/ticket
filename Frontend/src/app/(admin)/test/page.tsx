export interface Account {
  id: number;
  userId: number;
  providerId: number;
}

async function getAccounts(): Promise<Account[]> {
  const res = await fetch("https://localhost:5001/api/Account", {
    cache: "no-store",
  });

  if (!res.ok) {
    throw new Error("Błąd podczas pobierania danych");
  }

  return res.json();
}

export default async function AccountsPage() {
  const accounts = await getAccounts();

  return (
    <main className="p-10">
      <h1 className="mb-4 font-bold text-2xl">Lista Kont</h1>
      <div className="gap-4 grid">
        {accounts.map((acc) => (
          <div key={acc.id} className="shadow p-4 border rounded">
            <p>
              ID Konta: <strong>{acc.id}</strong>
            </p>
            <p>User ID: {acc.userId}</p>
          </div>
        ))}
      </div>
    </main>
  );
}
