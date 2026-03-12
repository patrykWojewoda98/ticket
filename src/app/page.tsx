import { TicketRepository } from "@/backend/infrastructure/ticket.repository";
import { TicketList } from "@/frontend/components/layout/ticketList";
import { Button } from "@/frontend/components/ui/button";
import { PrismaClient } from "@prisma/client";

export default async function Home() {
  const globalForPrisma = global as unknown as { prisma: PrismaClient };

  const prisma =
    globalForPrisma.prisma ??
    new PrismaClient({
      log: ["error"],
    });

  if (process.env.NODE_ENV !== "production") {
    globalForPrisma.prisma = prisma;
  }

  const repo = new TicketRepository(prisma);

  const tickets = await repo.findAll();
  const lastTickets = tickets.slice(0, 5);

  return (
    <div className="flex flex-col gap-16">
      {/* O nas */}
      <section className="flex flex-col gap-4">
        <h1 className="font-bold text-slate-900 text-3xl">Centrum wsparcia</h1>

        <p className="max-w-xl text-slate-600">Jeśli masz problem lub pytanie dotyczące naszych usług, utwórz ticket. Nasz zespół odpowie tak szybko jak to możliwe.</p>

        <div className="flex flex-col gap-1 text-slate-500 text-sm">
          <span>Email: support@myapp.com</span>
          <span>Telefon: +48 000 000 000</span>
        </div>
      </section>

      {/* Ostatnie tickety */}
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
