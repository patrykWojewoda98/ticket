import { notFound } from "next/navigation";

type Props = {
  params: Promise<{ id: string }>;
};

async function getTicket(id: number) {
  const res = await fetch(
    `${process.env.NEXT_PUBLIC_APP_URL}/api/Ticket/${id}`,
    { cache: "no-store" }
  );

  if (!res.ok) return null;
  return res.json();
}

async function getUser(id: number) {
  const res = await fetch(
    `${process.env.NEXT_PUBLIC_APP_URL}/api/user/${id}`,
    { cache: "no-store" }
  );
  return res.ok ? res.json() : null;
}

async function getStatus(id: number) {
  const res = await fetch(
    `${process.env.NEXT_PUBLIC_APP_URL}/api/ticketstatus/${id}`,
    { cache: "no-store" }
  );
  return res.ok ? res.json() : null;
}

async function getPriority(id: number) {
  const res = await fetch(
    `${process.env.NEXT_PUBLIC_APP_URL}/api/ticketpriority/${id}`,
    { cache: "no-store" }
  );
  return res.ok ? res.json() : null;
}

async function getCategory(id: number) {
  const res = await fetch(
    `${process.env.NEXT_PUBLIC_APP_URL}/api/ticketcategory/${id}`,
    { cache: "no-store" }
  );
  return res.ok ? res.json() : null;
}

export default async function TicketDetailPage({ params }: Props) {
  const { id } = await params;
  const ticketId = Number(id);

  if (isNaN(ticketId)) return notFound();

  const ticket = await getTicket(ticketId);
  if (!ticket) return notFound();

  const [user, status, priority, category] = await Promise.all([
    getUser(ticket.userId),
    getStatus(ticket.statusId),
    getPriority(ticket.priorityId),
    ticket.categoryId ? getCategory(ticket.categoryId) : null,
  ]);

  return (
    <div className="max-w-4xl mx-auto mt-16 px-6">
      {/* CARD */}
      <div className="bg-white border rounded-2xl shadow-lg p-8 flex flex-col gap-6">
        
        {/* TITLE */}
        <h1 className="text-3xl font-bold text-gray-900">
          {ticket.title}
        </h1>

        {/* META */}
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-6 text-sm">
          
          <div>
            <p className="text-gray-400 text-xs uppercase">User</p>
            <p className="font-medium text-gray-800">
              {user?.name ?? "Unknown"}
            </p>
          </div>

          <div>
            <p className="text-gray-400 text-xs uppercase">Status</p>
            <span className="px-3 py-1 rounded-full bg-blue-100 text-blue-700 text-sm font-medium">
              {status?.name ?? "Unknown"}
            </span>
          </div>

          <div>
            <p className="text-gray-400 text-xs uppercase">Priority</p>
            <span className="px-3 py-1 rounded-full bg-red-100 text-red-700 text-sm font-medium">
              {priority?.name ?? "Unknown"}
            </span>
          </div>

          <div>
            <p className="text-gray-400 text-xs uppercase">Category</p>
            <p className="font-medium text-gray-800">
              {category?.name ?? "No category"}
            </p>
          </div>
        </div>

        {/* DESCRIPTION */}
        <div>
          <p className="text-gray-400 text-xs uppercase mb-2">
            Description
          </p>
          <p className="text-gray-700 leading-relaxed whitespace-pre-wrap">
            {ticket.description}
          </p>
        </div>

      </div>
    </div>
  );
}