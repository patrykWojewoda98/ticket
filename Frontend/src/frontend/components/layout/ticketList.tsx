import { Ticket } from "@/backend/domain/ticket/ticket.entity";
import Link from "next/link";

interface Props {
  tickets: Ticket[];
}

export function TicketList({ tickets }: Props) {
  return (
    <div className="border border-slate-200 rounded-xl overflow-hidden">
      {tickets.map((ticket) => (
        <Link key={ticket.id} href={`/tickets/${ticket.id}`} className="flex justify-between items-center hover:bg-slate-50 px-6 py-4 last:border-0 border-b transition">
          <div className="flex flex-col">
            <span className="font-semibold text-slate-900">{ticket.title}</span>
            <span className="text-slate-500 text-sm">{ticket.statusId}</span>
          </div>

          <span className="text-slate-400 text-xs">{new Date(ticket.createdAt).toLocaleDateString()}</span>
        </Link>
      ))}
    </div>
  );
}
