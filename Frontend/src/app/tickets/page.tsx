import { mockTickets } from "@/lib/mock-tickets";
import { Button } from "@/components/ui/button";
import Link from "next/link";

export default function TicketsPage() {
  return (
    <div className="flex flex-col gap-8 mt-16">
      <div className="flex justify-between items-center">
        <h1 className="text-2xl font-bold text-slate-900">Moje tickety</h1>
      </div>

      <div className="grid gap-4">
        {mockTickets.map((ticket) => (
          <Link
            key={ticket.id}
            href={`/tickets/${ticket.id}`}
            className="flex justify-between items-center border rounded-xl p-4 hover:bg-slate-50 transition"
          >
            <div className="flex flex-col">
              <span className="font-medium text-slate-900">{ticket.title}</span>
              <span className="text-xs text-slate-500">{ticket.createdAt}</span>
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
          </Link>
        ))}
      </div>
    </div>
  );
}