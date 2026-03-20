"use client";

import Link from "next/link";
import { useEffect, useState } from "react";

type Ticket = {
  id: number;
  userId: number;
  assigneeId?: number | null;
  categoryId?: number | null;
  statusId: number;
  priorityId: number;
  title: string;
  description: string;
};

export default function Home() {
  const [tickets, setTickets] = useState<Ticket[]>([]);

  useEffect(() => {
    fetchTickets();
  }, []);

  const fetchTickets = async () => {
    try {
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_APP_URL}/api/ticket`
      );

      if (!res.ok) {
        console.error("API error:", res.status);
        return;
      }

      const text = await res.text();
      const data = text ? JSON.parse(text) : [];

      setTickets(data);
    } catch (error) {
      console.error("Error fetching tickets:", error);
    }
  };

  const getStatusLabel = (statusId: number) => {
    switch (statusId) {
      case 1:
        return { label: "OPEN", color: "text-green-600" };
      case 2:
        return { label: "PENDING", color: "text-yellow-600" };
      case 3:
        return { label: "CLOSED", color: "text-red-600" };
      default:
        return { label: "UNKNOWN", color: "text-gray-600" };
    }
  };

  return (
    <div className="flex flex-col gap-16 mt-16">
      <section className="flex flex-col gap-4">
        <h1 className="font-bold text-slate-900 text-3xl">
          Centrum wsparcia
        </h1>
      </section>

      <section>
        <h2 className="text-xl font-bold mb-4">Ostatnie tickety</h2>

        <table className="w-full border">
          <thead>
            <tr className="bg-gray-100">
              <th className="p-3 text-left">Title</th>
              <th className="p-3 text-left">Description</th>
              <th className="p-3 text-left">Status</th>
              <th className="p-3 text-left">Action</th>
            </tr>
          </thead>

          <tbody>
            {tickets
              .slice(-5)
              .reverse()
              .map((ticket) => {
                const status = getStatusLabel(ticket.statusId);

                return (
                  <tr key={ticket.id} className="border-t">
                    <td className="p-3">{ticket.title}</td>
                    <td className="p-3">{ticket.description}</td>

                    <td className={`p-3 font-semibold ${status.color}`}>
                      {status.label}
                    </td>
                    <td className="p-3">
                      <Link
                       href={`/customer/tickets/${ticket.id}`}
                        className="text-blue-600"
                      >
                        View
                      </Link>
                    </td>
                  </tr>
                );
              })}
          </tbody>
        </table>
      </section>
    </div>
  );
}