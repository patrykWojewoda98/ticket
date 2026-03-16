import Link from "next/link";

export default function AdminSidebar() {
  return (
    <aside className="w-64 bg-white border-r p-6 flex flex-col gap-4">

      <h2 className="text-xl font-bold mb-4">
        Admin Panel
      </h2>

      <Link href="/admin">Dashboard</Link>

      <Link href="/admin/tickets">
        Tickets
      </Link>

      <Link href="/admin/clients">
        Clients
      </Link>

    </aside>
  );
}