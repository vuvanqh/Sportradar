import Modal from "../UI/Modal"
import { useState, useEffect } from "react"
import usePlayers from "../../customHooks/usePlayers";
import { useLocations } from "../../customHooks/useLocations";
import useSports from "../../customHooks/useSports";
import useCompetitions from "../../customHooks/useCompetitions";
import Select from "react-select";
import { useActionState } from "react";
import { useCreateEvent } from "../../customHooks/useEvents";
import type { CreateFreeForAllEvent } from "../../types/createEventTypes";

type eventModalProps = {
    open: boolean,
    onClose: ()=>void
}

type SelectOption = {
    value: string;
    label: string;
};

export default function CreateFFAEvent({open = false, onClose}: eventModalProps){
    const {createEvent} = useCreateEvent();
    const [sport, setSport] = useState<string>("");
    const [competition, setCompetition] = useState<any>(null);
    const [location, setLocation] = useState<any>(null);
    const [playersSelected, setPlayers] = useState<SelectOption[]>([]);

    const { players } = usePlayers();
    const { locations } = useLocations();
    const { sports } = useSports();
    const { competitions } = useCompetitions();

    async function handleAction(_: any, formData: FormData) {
        const data = Object.fromEntries(formData.entries());
        const participants = formData.getAll("participants") as string[];

        if (!data.title || !data.startTime || !data.endTime)
        {
            console.log("error1");
            return { error: "Missing required fields" };
        }

        if (!data.locationId || data.locationId=="")
        {
            console.log("error2");
            return { error: "Location required" };
        }

        if (!data.sportId || data.sportId=="")
        {
            console.log("error3");
            return { error: "Sport required" };
        }

        if (participants.length < 2)
        {
            console.log("error4");
            return { error: "At least 2 participants required" };
        }

        const start = new Date(data.startTime as string);
        const end = new Date(data.endTime as string);

        if (start >= end){
            console.log("error5");
            return { error: "End time must be after start time" };
        }
        
        const competitionIdRaw = formData.get("competitionId");
        const competitionId =
        typeof competitionIdRaw === "string" && competitionIdRaw !== ""
            ? competitionIdRaw
            : undefined;

        const createEventReq: CreateFreeForAllEvent = {
            title: data.title as string,
            locationId: data.locationId as string,
            startTime: data.startTime as string,
            endTime: data.endTime as string,
            sportId: data.sportId as string,
            eventType: "FreeForAll",
            description: data.description as string | undefined,
            competitionId,
            participantIds: participants
        };
        console.log(createEventReq);
        try {
            console.log("Entering await");
            await createEvent(createEventReq);
            console.log("Exiting");
            return { success: true };
        } catch (e: any) {
            return { error: e.message };
        }
    }
    const [state, formAction] = useActionState(handleAction, null);
    const filteredComps = competitions.filter(c => c.sportId==sport);

    useEffect(() => {
        if (state?.success) onClose();
    }, [state]);
    
    return <Modal open={open} onClose={onClose}>
       <form className="event-form" action={formAction}>

            <input name="title" placeholder="Title" className="input" required />
            <div className="row one">
                <Select placeholder="Select location..." options={locations.map(l => ({ value: l.locationId, label: `${l.country} ${l.city} ${l.venue ?? ""}` }))} onChange={setLocation} />
                <input type="hidden" name="locationId" value={location?.value ?? ""} />
            </div>
                <input name="startTime" type="datetime-local" />
                <input name="endTime" type="datetime-local" />
            <div className="row">
                <Select placeholder="Select sport..." options={sports.map(s => ({ value: s.sportId, label: s.name }))} onChange={(val) => { setSport(val?.value ?? ""); setCompetition(null); }} />
                <input type="hidden" name="sportId" value={sport} />
            </div>
        
            <Select placeholder={sport ? "Select competition..." : "Select sport first"} isDisabled={!sport} options={filteredComps.map(c => ({ value: c.competitionId, label: c.name }))} onChange={setCompetition} />
            <input type="hidden" name="competitionId" value={competition?.value ?? ""} key={sport} />

            <Select isMulti placeholder="Select participants..." options={players.map(p => ({ value: p.playerId, label: `${p.firstName} ${p.lastName}` }))} onChange={(vals) => {setPlayers(vals as SelectOption[] || [])}} />

            {playersSelected.map(p => <input key={p.value} type="hidden" name="participants" value={p.value} />)}

            <textarea name="description" placeholder="Description" className="textarea" />

            <button type="submit"  className="submit-btn">Create</button>
            {state?.error && <p className="error">{state.error}</p>}
            </form>
    </Modal>
}
