import React, { useEffect, useState, FormEvent } from "react";
import { useParams, useNavigate } from "react-router-dom";

type School = {
    id: string;
    name: string;
    address?: string;
    phone?: string;
    email?: string;
    [key: string]: any;
};

export default function SchoolDetail(): JSX.Element {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();

    const [school, setSchool] = useState<School | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [saving, setSaving] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!id) {
            setError("No school id provided.");
            setLoading(false);
            return;
        }

        setLoading(true);
        setError(null);

        fetch(`/api/school/${encodeURIComponent(id)}`)
            .then((res) => {
                if (!res.ok) {
                    throw new Error(`Failed to load school (${res.status})`);
                }
                return res.json();
            })
            .then((data) => {
                setSchool(data);
            })
            .catch((err: Error) => {
                setError(err.message || "Failed to load school.");
            })
            .finally(() => setLoading(false));
    }, [id]);

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return (
            <div>
                <p style={{ color: "crimson" }}>Error: {error}</p>
                <button onClick={() => navigate(-1)}>Back</button>
            </div>
        );
    }

    if (!school) {
        return (
            <div>
                <p>School not found.</p>
                <button onClick={() => navigate("/school")}>Back to list</button>
            </div>
        );
    }

    return (
        <div style={{ maxWidth: 720, margin: "0 auto", padding: 16 }}>
            <h2>School Detail</h2>
            <form onSubmit={handleSubmit}>
                <div style={{ marginBottom: 12 }}>
                    <label>
                        Id
                        <br />
                        <input
                            name="id"
                            value={school.id ?? ""}
                            onChange={handleChange}
                            required
                            style={{ width: "100%", padding: 8 }}
                        />
                    </label>
                </div>

                <div style={{ marginBottom: 12 }}>
                    <label>
                        Name
                        <br />
                        <input
                            name="name"
                            value={school.name ?? ""}
                            onChange={handleChange}
                            required
                            style={{ width: "100%", padding: 8 }}
                        />
                    </label>
                </div>

                <div style={{ marginBottom: 12 }}>
                    <label>
                        Address
                        <br />
                        <textarea
                            name="address"
                            value={school.address ?? ""}
                            onChange={handleChange}
                            rows={3}
                            style={{ width: "100%", padding: 8 }}
                        />
                    </label>
                </div>

                <div style={{ display: "flex", gap: 8, marginBottom: 12 }}>
                    <label style={{ flex: 1 }}>
                        Phone
                        <br />
                        <input
                            name="studentCount"
                            value={school.studentCount ?? ""}
                            onChange={handleChange}
                            style={{ width: "100%", padding: 8 }}
                        />
                    </label>
                </div>

                <div style={{ display: "flex", gap: 8 }}>
                    <button type="submit" disabled={saving}>
                        {saving ? "Saving..." : "Save"}
                    </button>
                    <button type="button" onClick={() => navigate("/schoolList")} disabled={saving}>
                        Back to list
                    </button>
                </div>
            </form>
        </div>
    );
}