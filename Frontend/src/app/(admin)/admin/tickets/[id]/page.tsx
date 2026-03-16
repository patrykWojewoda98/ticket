export default function AdminTicketDetails({
  params,
}: {
  params: { id: string };
}) {

  const ticket = {
    id: params.id,
    title: "Printer not working",
    description: "The office printer is not responding.",
    company: "Acme Corp",
    priority: "high",
    status: "open",
  };

  return (
    <div>

      <h1 className="text-2xl font-bold mb-6">
        Ticket #{ticket.id}
      </h1>

      <div className="space-y-4">

        <div>
          <strong>Title:</strong> {ticket.title}
        </div>

        <div>
          <strong>Company:</strong> {ticket.company}
        </div>

        <div>
          <strong>Description:</strong>
          <p className="mt-1">{ticket.description}</p>
        </div>

        <div>
          <strong>Status:</strong> {ticket.status}
        </div>

        <div>
          <strong>Priority:</strong> {ticket.priority}
        </div>

      </div>

    </div>
  );
}