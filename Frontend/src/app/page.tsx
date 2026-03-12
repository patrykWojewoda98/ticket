import { Button } from "@/components/ui/button";
import Link from "next/link";

const tickets = [
  {
    id: "1",
    title: "Nie mogę się zalogować",
    status: "OPEN",
    createdAt: "2026-03-10",
  },
  {
    id: "2",
    title: "Problem z płatnością",
    status: "PENDING",
    createdAt: "2026-03-09",
  },
  {
    id: "3",
    title: "Błąd podczas dodawania projektu",
    status: "CLOSED",
    createdAt: "2026-03-07",
  },
];

export default function Home() {
  return (
    <div className="flex flex-col gap-16 mt-16">
      <section className="flex flex-col gap-4">
        <h1 className="font-bold text-slate-900 text-3xl">
          Centrum wsparcia
        </h1>

        <p className="max-w-xl text-slate-600">
          Jeśli masz problem lub pytanie dotyczące naszych usług, utwórz ticket.
          Nasz zespół odpowie tak szybko jak to możliwe.
        </p>

        <div className="flex flex-col gap-1 text-slate-500 text-sm">
          <span>Email: support@myapp.com</span>
          <span>Telefon: +48 000 000 000</span>
        </div>
      </section>

      <section className="flex flex-col gap-6">
        <div className="flex justify-between items-center">
          <h2 className="font-bold text-slate-900 text-xl">
            Ostatnie tickety
          </h2>
          
        </div>

        <div className="grid gap-3">
          {tickets.map((ticket) => (
            <div
              key={ticket.id}
              className="flex items-center justify-between rounded-xl border p-4 hover:bg-slate-50 transition"
            >
              <div className="flex flex-col">
                <span className="font-medium text-slate-900">
                  {ticket.title}
                </span>
                <span className="text-xs text-slate-500">
                  {ticket.createdAt}
                </span>
              </div>

              <span
                className={`text-xs font-medium px-2 py-1 rounded ${
                  ticket.status === "OPEN"
                    ? "bg-green-100 text-green-700"
                    : ticket.status === "PENDING"
                    ? "bg-yellow-100 text-yellow-700"
                    : "bg-slate-200 text-slate-700"
                }`}
              >
                {ticket.status}
              </span>
            </div>
          ))}
        </div>
      </section>
    </div>
  );
}