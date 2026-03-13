"use client";

import Link from "next/link";
import React from "react";

export type Ticket = {
  id: string;
  title: string;
  status: "OPEN" | "PENDING" | "CLOSED";
  createdAt: string;
};

type TicketListProps = {
  tickets: Ticket[];
};

export function TicketList({ tickets }: TicketListProps) {
  return (
    <div className="grid gap-4">
      {tickets.map((ticket) => (
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
            className={`ticket-badge ${
              ticket.status === "OPEN"
                ? "ticket-open"
                : ticket.status === "PENDING"
                ? "ticket-pending"
                : "ticket-closed"
            }`}
          >
            {ticket.status}
          </span>
        </Link>
      ))}
    </div>
  );
}