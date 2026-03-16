export default function NewClientPage() {
  return (
    <div>

      <h1 className="text-2xl font-bold mb-6">
        Create Client
      </h1>

      <form className="flex flex-col gap-4 max-w-md">

        <input
          type="text"
          placeholder="Company name"
          className="border p-2"
        />

        <input
          type="email"
          placeholder="Email"
          className="border p-2"
        />

        <button className="bg-black text-white p-2">
          Create
        </button>

      </form>

    </div>
  );
}