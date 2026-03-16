import { mockTickets } from "@/lib/mock-tickets";
import { TicketList } from "@/components/tickets/ticketList";

export default function TicketsPage() {
  return (
    <div className="flex flex-col gap-8 mt-16">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold text-slate-900">Moje tickety</h1>
      </div>

      <TicketList tickets={mockTickets} />
    </div>
  );
}