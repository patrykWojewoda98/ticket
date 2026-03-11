import { ReactNode } from "react";
import { Poppins } from "next/font/google";
import Header from "@/components/header";
import Footer from "@/components/footer";
import "./globals.css";

const poppins = Poppins({
  subsets: ["latin", "latin-ext"],
  weight: ["400", "700"],
  display: "swap",
});

interface LayoutProps {
  children: ReactNode;
}

export default function Layout({ children }: LayoutProps) {
  return (
    <html lang="pl">
      <body className={poppins.className}>
        
        <div className="flex flex-col min-h-screen mx-auto px-[10%] 2xl:px-32 min-w-75 max-w-325">
          
          
          <Header />

        
          <main className="flex-grow">
            {children}
          </main>

          
          <Footer />
        </div>
      </body>
    </html>
  );
}