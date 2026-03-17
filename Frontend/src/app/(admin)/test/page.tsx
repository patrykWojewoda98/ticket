export interface Account {
  id: number;
  userId: number;
  providerId: number;
}

async function getAccount(id: number) {
  const res = await fetch(`http://localhost:5229/api/account/user/${id}`, {
    cache: "no-store",
  });

  if (!res.ok) {
    if (res.status === 404) return null;
    throw new Error("Błąd podczas pobierania danych");
  }

  return res.json();
}

export default async function AccountPage() {
  const account = await getAccount(1);

  if (!account) {
    return <div>Nie znaleziono konta o ID: 1</div>;
  }

  return (
    <div>
      <h1>Konto: {account.providerId}</h1>
      <p>User ID: {account.userId}</p>
      <pre>{JSON.stringify(account, null, 2)}</pre>
    </div>
  );
}
