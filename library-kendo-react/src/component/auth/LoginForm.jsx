import { useState } from "react";
import { Input } from "@progress/kendo-react-inputs";
import { Button } from "@progress/kendo-react-buttons";

export default function LoginForm({ onLogin }) {
    const [username, setUsername] = useState("admin");
    const [password, setPassword] = useState("Admin123!");
    const [msg, setMsg] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            await onLogin(username, password);
        } catch {
            setMsg("Login gagal");
        }
    };

    return (
        <div style={{ maxWidth: 360, margin: "40px auto", padding: 24, border: "1px solid #ddd" }}>
            <h2>Login</h2>
            <form onSubmit={handleSubmit}>
                <div style={{ marginBottom: 12 }}>
                    <label>Username</label>
                    <Input value={username} onChange={(e) => setUsername(e.value)} />
                </div>
                <div style={{ marginBottom: 12 }}>
                    <label>Password</label>
                    <Input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.value)}
                    />
                </div>
                {msg && <p style={{ color: "red" }}>{msg}</p>}
                <Button type="submit" themeColor="primary">Login</Button>
            </form>
        </div>
    );
}
