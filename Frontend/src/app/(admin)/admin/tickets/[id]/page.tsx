async function getTicket(id: string) {
  const res = await fetch(`http://localhost:5229/api/ticket/${id}`, {
    cache: "no-store",
  });

  if (!res.ok) throw new Error("Failed to fetch ticket");

  return res.json();
}

async function getUser(userId: number) {
  const res = await fetch(`http://localhost:5229/api/user/${userId}`, {
    cache: "no-store",
  });

  if (!res.ok) throw new Error("Failed to fetch user");

  return res.json();
}

async function getStatus(statusId: number) {
  const res = await fetch(`http://localhost:5229/api/ticketstatus/${statusId}`, {
    cache: "no-store",
  });

  if (!res.ok) throw new Error("Failed to fetch status");

  return res.json();
}

async function getPriority(priorityId: number) {
  const res = await fetch(`http://localhost:5229/api/ticketpriority/${priorityId}`, {
    cache: "no-store",
  });

  if (!res.ok) throw new Error("Failed to fetch priority");

  return res.json();
}

async function getCategory(categoryId: number) {
  const res = await fetch(`http://localhost:5229/api/ticketcategory/${categoryId}`, {
    cache: "no-store",
  });

  if (!res.ok) throw new Error("Failed to fetch category");

  return res.json();
}

export default async function AdminTicketDetails({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;

  const ticket = await getTicket(id);

  const [user, status, priority, category] = await Promise.all([
    getUser(ticket.userId),
    getStatus(ticket.statusId),
    getPriority(ticket.priorityId),
    ticket.categoryId ? getCategory(ticket.categoryId) : Promise.resolve(null),
  ]);

  return (
  <div className="mt-16 max-w-4xl mx-auto px-4">
    <h1 className="text-3xl font-bold mb-8 text-gray-900">
      Ticket Details
    </h1>

    <div className="bg-white rounded-2xl shadow-md border border-gray-200 p-8 space-y-8">
      
      
      <div>
        <p className="text-xs uppercase tracking-wide text-gray-400">
          Title
        </p>
        <p className="text-xl font-semibold text-gray-900">
          {ticket.title}
        </p>
      </div>

     
      <div className="grid grid-cols-1 sm:grid-cols-2 gap-6">
        
        <div>
          <p className="text-xs uppercase text-gray-400">User</p>
          <p className="font-medium text-gray-800">{user.name}</p>
        </div>

        <div>
          <p className="text-xs uppercase text-gray-400">Status</p>
          <span className="inline-block px-3 py-1 text-sm rounded-full bg-blue-100 text-blue-700">
            {status.name}
          </span>
        </div>

        <div>
          <p className="text-xs uppercase text-gray-400">Priority</p>
          <span className="inline-block px-3 py-1 text-sm rounded-full bg-red-100 text-red-700">
            {priority.name}
          </span>
        </div>

        <div>
          <p className="text-xs uppercase text-gray-400">Category</p>
          <p className="font-medium text-gray-800">
            {category ? category.name : "No category"}
          </p>
        </div>
      </div>

      
      <div>
        <p className="text-xs uppercase text-gray-400 mb-2">
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