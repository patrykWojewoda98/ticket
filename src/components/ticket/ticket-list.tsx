import { Ticket } from "@/domain/ticket/ticket.entity";
import Link from "next/link";

interface Props {
  tickets: Ticket[];
}

export function TicketList({ tickets }: Props) {
  return (
    <div className="border border-slate-200 rounded-xl overflow-hidden">

      {tickets.map((ticket) => (
        <Link
          key={ticket.id}
          href={`/tickets/${ticket.id}`}
          className="flex items-center justify-between px-6 py-4 border-b last:border-0 hover:bg-slate-50 transition"
        >
          <div className="flex flex-col">
            <span className="font-semibold text-slate-900">
              {ticket.title}
            </span>

            <span className="text-sm text-slate-500">
              {ticket.statusId}
            </span>
          </div>

          <span className="text-xs text-slate-400">
            {new Date(ticket.createdAt).toLocaleDateString()}
          </span>
        </Link>
      ))}

    </div>
  );
}