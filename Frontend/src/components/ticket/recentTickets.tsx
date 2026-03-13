"use client";

import React from "react";

export type Ticket = {
  id: string;
  title: string;
  status: "OPEN" | "PENDING" | "CLOSED";
  createdAt: string;
};

type RecentTicketsProps = {
  tickets: Ticket[];
};

export function RecentTickets({ tickets }: RecentTicketsProps) {
  return (
    <div className="grid gap-3">
      {tickets.map((ticket) => (
        <div
          key={ticket.id}
          className="flex items-center justify-between rounded-xl border p-4 hover:bg-slate-50 transition"
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
        </div>
      ))}
    </div>
  );
}