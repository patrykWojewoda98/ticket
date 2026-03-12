"use client";

import { useEffect, useState } from "react";
import Link from "next/link";

type Ticket = {
  id: string;
  title: string;
  status: "OPEN" | "CLOSED" | "PENDING";
  createdAt: string;
};

export function RecentTickets() {
  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadTickets = async () => {
      try {
        const res = await fetch("/api/tickets?limit=5");
        const data = await res.json();
        setTickets(data);
      } catch (e) {
        console.error(e);
      } finally {
        setLoading(false);
      }
    };

    loadTickets();
  }, []);

  if (loading) {
    return (
      <div className="text-sm text-slate-500">
        Wczytywanie ticketów...
      </div>
    );
  }

  if (!tickets.length) {
    return (
      <div className="text-sm text-slate-500">
        Brak ticketów.
      </div>
    );
  }

  return (
    <div className="grid gap-3">
      {tickets.map((ticket) => (
        <Link
          key={ticket.id}
          href={`/tickets/${ticket.id}`}
          className="flex items-center justify-between rounded-xl border p-4 hover:bg-slate-50 transition"
        >
          <div className="flex flex-col">
            <span className="font-medium text-slate-900">
              {ticket.title}
            </span>
            <span className="text-xs text-slate-500">
              {new Date(ticket.createdAt).toLocaleDateString()}
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
        </Link>
      ))}
    </div>
  );
}