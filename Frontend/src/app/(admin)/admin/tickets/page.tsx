import Link from "next/link";
import { mockTickets, TicketStatus } from "@/lib/mock-tickets";

export default function AdminTicketsPage() {

  const getStatusColor = (status: TicketStatus) => {
    switch (status) {
      case "OPEN":
        return "text-green-600 font-semibold";
      case "PENDING":
        return "text-yellow-600 font-semibold";
      case "CLOSED":
        return "text-red-600 font-semibold";
      default:
        return "";
    }
  };

  return (
    <div>
      <h1 className="text-2xl font-bold mb-6">
        Tickets
      </h1>

      <table className="w-full border">
        <thead>
          <tr className="bg-gray-100">
            <th className="p-3 text-left">Title</th>
            <th className="p-3 text-left">Created At</th>
            <th className="p-3 text-left">Status</th>
            <th className="p-3 text-left">Action</th>
          </tr>
        </thead>

        <tbody>
          {mockTickets.map((ticket) => (
            <tr key={ticket.id} className="border-t">
              <td className="p-3">{ticket.title}</td>
              <td className="p-3">{ticket.createdAt}</td>
              <td className={`p-3 ${getStatusColor(ticket.status)}`}>
                {ticket.status}
              </td>
              <td className="p-3">
                <Link href={`/admin/tickets/${ticket.id}`}>
                  View
                </Link>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}