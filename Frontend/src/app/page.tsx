import { Button } from "@/components/ui/button";
import { TicketList } from "@/components/common/ticketList";

export default async function Home() {
  // const tickets = await repo.findAll();
  // const lastTickets = tickets.slice(0, 5);

  return (
    <div className="flex flex-col gap-16">
      <section className="flex flex-col gap-4">
        <h1 className="font-bold text-slate-900 text-3xl">Centrum wsparcia</h1>

        <p className="max-w-xl text-slate-600">Jeśli masz problem lub pytanie dotyczące naszych usług, utwórz ticket. Nasz zespół odpowie tak szybko jak to możliwe.</p>

        <div className="flex flex-col gap-1 text-slate-500 text-sm">
          <span>Email: support@myapp.com</span>
          <span>Telefon: +48 000 000 000</span>
        </div>
      </section>

      <section className="flex flex-col gap-6">
        <div className="flex justify-between items-center">
          <h2 className="font-bold text-slate-900 text-xl">Ostatnie tickety</h2>

          <Button variant="outline" asChild>
            <a href="/tickets">Zobacz wszystkie</a>
          </Button>
        </div>

        <TicketList tickets={lastTickets} />
      </section>
    </div>
  );
}
