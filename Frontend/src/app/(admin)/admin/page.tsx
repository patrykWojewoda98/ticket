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

export default function AdminHomePage() {
  const [tickets, setTickets] = useState<Ticket[]>([]);

  useEffect(() => {
    fetchTickets();
  }, []);

  const fetchTickets = async () => {
    try {
      const res = await fetch(`${process.env.NEXT_PUBLIC_APP_URL}/api/ticket`);

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

  const ticketCounts = {
    OPEN: tickets.filter(t => t.statusId === 1).length,
    PENDING: tickets.filter(t => t.statusId === 2).length,
    CLOSED: tickets.filter(t => t.statusId === 3).length,
  };

  return (
    <div className="p-6 space-y-8">
      <h1 className="text-3xl font-bold">Admin Panel</h1>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        <div className="p-4 bg-green-100 rounded-lg shadow">
          <h2 className="text-xl font-semibold">Open Tickets</h2>
          <p className="text-2xl font-bold">{ticketCounts.OPEN}</p>
        </div>
        <div className="p-4 bg-yellow-100 rounded-lg shadow">
          <h2 className="text-xl font-semibold">Pending Tickets</h2>
          <p className="text-2xl font-bold">{ticketCounts.PENDING}</p>
        </div>
        <div className="p-4 bg-red-100 rounded-lg shadow">
          <h2 className="text-xl font-semibold">Closed Tickets</h2>
          <p className="text-2xl font-bold">{ticketCounts.CLOSED}</p>
        </div>
      </div>

      <div>
        <h2 className="text-2xl font-bold mb-4">Latest Tickets</h2>

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
            {tickets.slice(-5).reverse().map(ticket => {
              const status = getStatusLabel(ticket.statusId);

              return (
                <tr key={ticket.id} className="border-t">
                  <td className="p-3">{ticket.title}</td>
                  <td className="p-3">{ticket.description}</td>

                  <td className={`p-3 font-semibold ${status.color}`}>
                    {status.label}
                  </td>

                  <td className="p-3">
                    <Link href={`/admin/tickets/${ticket.id}`} className="text-blue-600">
                      View
                    </Link>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
}