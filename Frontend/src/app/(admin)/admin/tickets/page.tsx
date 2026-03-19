"use client";

import Link from "next/link";
import { useEffect, useState } from "react";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";

type Ticket = {
  id: number;
  priorityId:number;
  title: string;
  userId: number;
  assigneeId: number;
  categoryId: number;
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

  
const updateTicketStatus = async (ticketId: number, statusId: number) => {
  const ticket = tickets.find(t => t.id === ticketId);

  const res = await fetch(`http://localhost:5229/api/ticket/${ticketId}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      title: ticket?.title,
      description: ticket?.description,
      statusId: statusId,
      priorityId: ticket?.priorityId,
      userId: ticket?.userId,
      assigneeId: ticket?.assigneeId,
      categoryId: ticket?.categoryId,
    }),
  });

  if (!res.ok) {
    console.error("Update failed:", res.status);
  }

  await fetchTickets();
};
const updateTicketPriority = async (ticketId: number, priorityId: number) => {
  const ticket = tickets.find(t => t.id === ticketId);

  try {
    const res = await fetch(`http://localhost:5229/api/ticket/${ticketId}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        title: ticket?.title,
        description: ticket?.description,
        statusId: ticket?.statusId,
        priorityId: priorityId,
        userId: ticket?.userId,
        assigneeId: ticket?.assigneeId,
        categoryId: ticket?.categoryId,
      }),
    });

    if (!res.ok) {
      console.error("Priority update failed:", res.status);
    }

    await fetchTickets();
  } catch (error) {
    console.error("Error updating priority:", error);
  }
};
const getPriorityLabel = (priorityId: number) => {
  switch (priorityId) {
    case 1:
      return { label: "LOW", color: "bg-blue-100 text-blue-700 border-blue-200" };
    case 2:
      return { label: "MEDIUM", color: "bg-yellow-100 text-yellow-700 border-yellow-200" };
    case 3:
      return { label: "HIGH", color: "bg-red-100 text-red-700 border-red-200" };
    default:
      return { label: "UNKNOWN", color: "bg-gray-100 text-gray-700 border-gray-200" };
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
const handlePriorityChange = (id: number, newPriorityId: number) => {
  setTickets((prev) =>
    prev.map((t) =>
      t.id === id ? { ...t, priorityId: newPriorityId } : t
    )
  );

  updateTicketPriority(id, newPriorityId);
};
const handleStatusChange = (id: number, newStatusId: number) => {
  
  setTickets((prev) =>
    prev.map((t) =>
      t.id === id ? { ...t, statusId: newStatusId } : t
    )
  );

  
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
              <th className="p-3">Priority</th>
              <th className="p-3">Action</th>
            </tr>
          </thead>

          <tbody>
            {tickets.map((ticket) => {
              const status = getStatusLabel(ticket.statusId);
              const priority = getPriorityLabel(ticket.priorityId);

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
  <Select
    value={ticket.priorityId.toString()}
    onValueChange={(value) =>
      handlePriorityChange(ticket.id, Number(value))
    }
  >
    <SelectTrigger className="w-[140px] rounded-lg border bg-white px-3 py-1 text-sm shadow-sm hover:border-gray-400 transition">
      <SelectValue >
        <span
  className={`inline-flex items-center justify-center w-24 px-2 py-1 rounded-full text-xs font-semibold border ${priority.color}`}
>
  {priority.label}
</span>
        </SelectValue>
    </SelectTrigger>

    <SelectContent>
      <SelectItem value="1">LOW</SelectItem>
      <SelectItem value="2">MEDIUM</SelectItem>
      <SelectItem value="3">HIGH</SelectItem>
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