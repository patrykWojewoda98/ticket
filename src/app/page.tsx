import { TicketRepository } from "@/infrastructure/ticket.repository";
import { TicketList } from "@/components/ticket/ticket-list";
import { Button } from "@/components/ui/button";
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
        <h1 className="text-3xl font-bold text-slate-900">
          Centrum wsparcia
        </h1>

        <p className="text-slate-600 max-w-xl">
          Jeśli masz problem lub pytanie dotyczące naszych usług,
          utwórz ticket. Nasz zespół odpowie tak szybko jak to możliwe.
        </p>

        <div className="flex flex-col gap-1 text-sm text-slate-500">
          <span>Email: support@myapp.com</span>
          <span>Telefon: +48 000 000 000</span>
        </div>
      </section>

      {/* Ostatnie tickety */}
      <section className="flex flex-col gap-6">

        <div className="flex items-center justify-between">
          <h2 className="text-xl font-bold text-slate-900">
            Ostatnie tickety
          </h2>

          <Button variant="outline" asChild>
            <a href="/tickets">Zobacz wszystkie</a>
          </Button>
        </div>

        <TicketList tickets={lastTickets} />

      </section>

    </div>
  );
}