import { useCallback, useEffect, useState } from "react";
import type { School } from "../types/school";
import { useParams, useNavigate } from "react-router-dom";
function SchoolList() {
    const [schools, setSchools] = useState<School[]>([]);
    const navigate = useNavigate();

    const fetchSchools = useCallback(async () => {
        try {
            const response = await fetch("/api/school");
            if (response.ok) {
                const data = await response.json();
                setSchools(data);
            }
        } catch (error) {
            console.error("Fetch error:", error);
        }
    }, []);

    useEffect(() => {
        const fetchSchools = async () => {
            const response = await fetch("/api/school");
            const data = await response.json();
            setSchools(data);
        };
        fetchSchools();
    }, []);

    return (
    <table>
        <div>
            <h1>School List</h1>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Address</th>
                    <th>Student Count</th>
                </tr>
            </thead>
            <tbody>
                {schools.map((school) => (
                    <tr key={school.id}>
                        <td>{school.id}</td>
                        <td>{school.name}</td>
                        <td>{school.address}</td>
                        <td>{school.studentCount}</td>
                        <td>
                            <button type="button" className="btn btn-primary" onClick={() => navigate(`/schoolDetails/${school.id}`)}>
                                View Details
                            </button>
                            <button type="button" className="btn btn-primary" onClick={() => navigate(`/schoolDelete/${school.id}`)}>
                                Delete
                            </button>
                        </td>

                    </tr>
                ))}
            </tbody>
        </div>
    </table>);
}

export default SchoolList;