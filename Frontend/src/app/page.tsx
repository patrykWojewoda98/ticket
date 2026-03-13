import { mockTickets } from "@/lib/mock-tickets";
import { RecentTickets } from "@/components/ticket/recentTickets";

export default function Home() {
  const tickets = mockTickets.slice(0, 3); // tylko pierwsze 3 tickety

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
          <h2 className="font-bold text-slate-900 text-xl">Ostatnie tickety</h2>
        </div>

        <RecentTickets tickets={tickets} />
      </section>
    </div>
  );
}