"use client"
import SignalR from "@/app/components/websockets/signalR"

export default function GymGoerMessages() {
  return (
    <main>
      <h1>Messages:</h1>
      <SignalR />
    </main>
  );
}