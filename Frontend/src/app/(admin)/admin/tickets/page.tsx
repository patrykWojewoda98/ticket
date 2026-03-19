"use client";

import Link from "next/link";
import { useEffect, useState } from "react";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";

type Ticket = {
  id: number;
  title: string;
  description: string;
  createdAt?: string;
  statusId: number;
};

export default function AdminTicketsPage() {
  const [tickets, setTickets] = useState<Ticket[]>([]);

  useEffect(() => {
    fetchTickets();
  }, []);

  const fetchTickets = async () => {
    try {
      const res = await fetch("http://localhost:5229/api/ticket");

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

  // ✅ POPRAWIONE — aktualizacja ticketu, nie TicketStatus
const updateTicketStatus = async (ticketId: number, statusId: number) => {
  try {
    const res = await fetch(`http://localhost:5229/api/ticket/${ticketId}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        statusId: statusId,
      }),
    });

    if (!res.ok) {
      console.error("Update failed:", res.status);
    }

    // 👉 drugi fetch (odświeżenie listy)
    await fetchTickets();

  } catch (error) {
    console.error("Error updating ticket:", error);
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

const handleStatusChange = (id: number, newStatusId: number) => {
  // update UI
  setTickets((prev) =>
    prev.map((t) =>
      t.id === id ? { ...t, statusId: newStatusId } : t
    )
  );

  // update backend + refresh
  updateTicketStatus(id, newStatusId);
};

  return (
    <div>
      <h1 className="text-2xl font-bold mb-6 mt-16">Tickets</h1>

      <div className="overflow-hidden rounded-2xl border shadow-sm">
        <table className="w-full">
          <thead>
            <tr className="bg-gray-100 text-left text-sm">
              <th className="p-3">Title</th>
              <th className="p-3">Description</th>
              <th className="p-3">Status</th>
              <th className="p-3">Action</th>
            </tr>
          </thead>

          <tbody>
            {tickets.map((ticket) => {
              const status = getStatusLabel(ticket.statusId);

              return (
                <tr key={ticket.id} className="border-t hover:bg-gray-50">
                  <td className="p-3 font-medium">{ticket.title}</td>
                  <td className="p-3 text-gray-600">{ticket.description}</td>

                  <td className="p-3">
                    <Select
                      value={ticket.statusId.toString()}
                      onValueChange={(value) =>
                        handleStatusChange(ticket.id, Number(value))
                      }
                    >
                      <SelectTrigger className="px-2 py-1 rounded-lg text-sm border">
                        <SelectValue>
                          <span className={status.color}>
                            {status.label}
                          </span>
                        </SelectValue>
                      </SelectTrigger>

                      <SelectContent>
                        <SelectItem value="1">OPEN</SelectItem>
                        <SelectItem value="2">PENDING</SelectItem>
                        <SelectItem value="3">CLOSED</SelectItem>
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
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
}