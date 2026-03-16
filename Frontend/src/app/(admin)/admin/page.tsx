import Link from "next/link";
import { mockTickets } from "@/lib/mock-tickets";
import { mockUsers } from "@/lib/mock-client";

export default function AdminHomePage() {

  const ticketCounts = {
    OPEN: mockTickets.filter(t => t.status === "OPEN").length,
    PENDING: mockTickets.filter(t => t.status === "PENDING").length,
    CLOSED: mockTickets.filter(t => t.status === "CLOSED").length,
  };

  const totalClients = mockUsers.length;

  return (
    <div className="p-6 space-y-8">

      <h1 className="text-3xl font-bold">Admin Panel</h1>

     
      <div className="grid grid-cols-1 md:grid-cols-4 gap-6">
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
        <div className="p-4 bg-blue-100 rounded-lg shadow">
          <h2 className="text-xl font-semibold">Total Clients</h2>
          <p className="text-2xl font-bold">{totalClients}</p>
        </div>
      </div>

      
      <div>
        <h2 className="text-2xl font-bold mb-4">Latest Tickets</h2>
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
            {mockTickets.slice(-5).reverse().map(ticket => (
              <tr key={ticket.id} className="border-t">
                <td className="p-3">{ticket.title}</td>
                <td className="p-3">{ticket.createdAt}</td>
                <td className={`p-3 font-semibold ${
                  ticket.status === "OPEN" ? "text-green-600" :
                  ticket.status === "PENDING" ? "text-yellow-600" :
                  "text-red-600"
                }`}>
                  {ticket.status}
                </td>
                <td className="p-3">
                  <Link href={`/admin/tickets/${ticket.id}`} className="text-blue-600">
                    View
                  </Link>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

     
      <div className="flex flex-wrap gap-4 mt-6">
        <Link href="/admin/tickets" className="px-4 py-2 bg-black text-white rounded">All Tickets</Link>
        <Link href="/admin/clients" className="px-4 py-2 bg-black text-white rounded">All Clients</Link>
        <Link href="/admin/clients/new" className="px-4 py-2 bg-black text-white rounded">New Client</Link>
      </div>

    </div>
  );
}