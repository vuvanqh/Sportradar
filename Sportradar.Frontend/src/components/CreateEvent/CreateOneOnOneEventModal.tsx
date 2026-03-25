import Modal from "../UI/Modal"
import { useState, useEffect } from "react"
import usePlayers from "../../customHooks/usePlayers";
import { useLocations } from "../../customHooks/useLocations";
import useSports from "../../customHooks/useSports";
import useCompetitions from "../../customHooks/useCompetitions";
import Select from "react-select";
import { useActionState } from "react";
import { useCreateEvent } from "../../customHooks/useEvents";
import type { CreateOneOnOneEvent } from "../../types/createEventTypes";

type eventModalProps = {
    open: boolean,
    onClose: ()=>void
}

export default function CreateOneOnOneEvent({open = false, onClose}: eventModalProps){
    const {createEvent} = useCreateEvent();
    const [sport, setSport] = useState<string>("");
    const [homePlayer, setHomePlayer] = useState<string>("");
    const [awayPlayer, setAwayPlayer] = useState<string>("");
    const [competition, setCompetition] = useState<any>(null);
    const [location, setLocation] = useState<any>(null);

    const { players } = usePlayers();
    const { locations } = useLocations();
    const { sports } = useSports();
    const { competitions } = useCompetitions();
    const filteredComps = competitions.filter(c => c.sportId==sport);

    async function handleAction(_: any, formData: FormData) {
        const data = Object.fromEntries(formData.entries());

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

        if (!homePlayer || !awayPlayer) {
            return { error: "Both players must be selected" };
        }

        if (homePlayer === awayPlayer) {
            return { error: "Players must be different" };
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

        const createEventReq: CreateOneOnOneEvent = {
            title: data.title as string,
            locationId: data.locationId as string,
            startTime: data.startTime as string,
            endTime: data.endTime as string,
            sportId: data.sportId as string,
            eventType: "OneOnOne",
            description: data.description as string | undefined,
            competitionId,
            homePlayerId: homePlayer,
            awayPlayerId: awayPlayer,
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

            <Select placeholder={sport ? "Select home player..." : "Select sport first"} isDisabled={!sport} options={players.filter(p=>p.playerId!=awayPlayer).map(c => ({ value: c.playerId, label: `${c.firstName} ${c.lastName}` }))} onChange={(val) => setHomePlayer(val?.value ?? "")} />
            <Select placeholder={sport ? "Select away player..." : "Select sport first"} isDisabled={!sport} options={players.filter(p=>p.playerId!=homePlayer).map(c => ({ value: c.playerId, label: `${c.firstName} ${c.lastName}` }))} onChange={(val) => setAwayPlayer(val?.value ?? "")} />
            <input type="hidden" name="homePlayerId" value={homePlayer} />
            <input type="hidden" name="awayPlayerId" value={awayPlayer} />

            <textarea name="description" placeholder="Description" className="textarea" />

            <button type="submit"  className="submit-btn">Create</button>
            {state?.error && <p className="error">{state.error}</p>}
            </form>
    </Modal>
}
