"use client";

import Link from "next/link";
import { useState } from "react";
import { mockTickets, TicketStatus } from "@/lib/mock-tickets";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";

export default function AdminTicketsPage() {
  const [tickets, setTickets] = useState(mockTickets);

  const getStatusColor = (status: TicketStatus) => {
    switch (status) {
      case "OPEN":
        return "bg-green-100 text-green-700";
      case "PENDING":
        return "bg-yellow-100 text-yellow-700";
      case "CLOSED":
        return "bg-red-100 text-red-700";
      default:
        return "";
    }
  };

  const handleStatusChange = (id: string, newStatus: TicketStatus) => {
    const updated = tickets.map((t) =>
      t.id.toString() === id ? { ...t, status: newStatus } : t
    );
    setTickets(updated);
  };

  return (
    <div>
      <h1 className="text-2xl font-bold mb-6 mt-16">Tickets</h1>

      <div className="overflow-hidden rounded-2xl border shadow-sm">
        <table className="w-full">
          <thead>
            <tr className="bg-gray-100 text-left text-sm">
              <th className="p-3">Title</th>
              <th className="p-3">Created At</th>
              <th className="p-3">Status</th>
              <th className="p-3">Action</th>
            </tr>
          </thead>

          <tbody>
            {tickets.map((ticket) => (
              <tr key={ticket.id} className="border-t hover:bg-gray-50">
                <td className="p-3 font-medium">{ticket.title}</td>
                <td className="p-3 text-gray-600">{ticket.createdAt}</td>

                <td className="p-3">
                  <Select
                    value={ticket.status}
                    onValueChange={(value) =>
                      handleStatusChange(ticket.id.toString(), value as TicketStatus)
                    }
                  >
                    <SelectTrigger
                      className={`px-2 py-1 rounded-lg text-sm border ${getStatusColor(ticket.status)}`}
                    >
                      <SelectValue />
                    </SelectTrigger>
                    <SelectContent>
                      <SelectItem value="OPEN">OPEN</SelectItem>
                      <SelectItem value="PENDING">PENDING</SelectItem>
                      <SelectItem value="CLOSED">CLOSED</SelectItem>
                    </SelectContent>
                  </Select>
                </td>

                <td className="p-3">
                  <Link
                    href={`/admin/tickets/${ticket.id}`}
                    className="text-blue-600 hover:underline text-sm"
                  >
                    View
                  </Link>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}