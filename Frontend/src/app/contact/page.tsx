import { Button } from "@/components/ui/button";
import { mockMessages, type Message } from "@/lib/mock-messages";

interface HomeProps {
  conversationId?: string;
}

export default function Home({ conversationId = "1" }: HomeProps) {
  
  const messages: Message[] = mockMessages[conversationId] || [];

  return (
    <div className="flex flex-col h-screen bg-gray-100 p-4">
      <h1 className="text-2xl font-bold mb-4">Czat z adminem</h1>

      
      <div className="flex-1 overflow-y-auto space-y-2 p-2 border rounded-lg bg-white">
        {messages.map((msg) => (
          <div
            key={msg.id}
            className={`flex ${
              msg.author === "CUSTOMER" ? "justify-end" : "justify-start"
            }`}
          >
            <div
              className={`max-w-xs px-4 py-2 rounded-lg break-words ${
                msg.author === "CUSTOMER"
                  ? "bg-blue-500 text-white rounded-br-none"
                  : "bg-gray-300 text-gray-900 rounded-bl-none"
              }`}
            >
              {msg.content}
              <div className="text-xs text-gray-600 mt-1">{msg.createdAt}</div>
            </div>
          </div>
        ))}
      </div>

      
      <div className="mt-4 flex gap-2">
        <input
          type="text"
          placeholder="Napisz wiadomość..."
          className="flex-1 px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-400"
        />
        <Button>Wyślij</Button>
      </div>
    </div>
  );
}