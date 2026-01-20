import { useCallback, useEffect, useState } from "react";
import type { School } from "../types/school";
import { useParams, useNavigate } from "react-router-dom";
function SchoolDelete() {
    const { id } = useParams<{ id: string }>();
    const [schools, setSchools] = useState<School[]>([]);
    const navigate = useNavigate();
    const fetchSchools = useCallback(async () => {
        try {
            const response = await fetch(`/api/school/${id}`);
            if (!response.ok) {
                const data = await response.json();
                setSchools(data);
            }
        } catch (error) {
            console.error("Error fetching school:", error);
        }
    }, [id]);
}

export default SchoolDelete