import { useCallback, useEffect, useState } from "react";
import type { School } from "../types/school";
import { useParams, useNavigate } from "react-router-dom";

function SchoolDelete() {
    const { id } = useParams<{ id: string }>();
    const [school, setSchool] = useState<School | null>(null);
    const navigate = useNavigate();
    const fetchSchool = useCallback(async () => {
        try {
            const response = await fetch(`/api/school/${id}`);
            if (response.ok) {
                const data = await response.json();
                setSchool(data);
            }
        } catch (error) {
            console.error("Error fetching school:", error);
        }
    }, [id]);

    useEffect(() => {
        (async () => {
            await fetchSchool();
        })()
    }, [fetchSchool]);

    if (!school) {
        return <div>Loading...</div>;
    }
    return (
        <div>
            <button onClick={() => navigate("/")} style={{ marginBottom: '20px' }}>
                Back to List
            </button>
            <div>
                <h1>School Delete</h1>
                <p><strong>ID:</strong> {school.id}</p>
                <p><strong>Name:</strong> {school.name}</p>
                <p><strong>Address:</strong> {school.address}</p>
                <p><strong>Student Count:</strong> {school.studentCount}</p>
            </div>
        </div>
    )
}

export default SchoolDelete