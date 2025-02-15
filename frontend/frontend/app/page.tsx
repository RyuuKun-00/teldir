import { HomeImage } from "./components/HomeImage";
import "./pageHome.css"

export default function Home() {
  return (
    <div className="ContentHome">
      <div className="Content">
        <p className="title">Проект сделан в образовательных целях!</p>
        <p className="text">С использованием следующих средств и технологий:</p>
        <div className="technologies">
          <HomeImage 
            src="/images/doc.png" 
            alt="Docker"
            />
          <HomeImage 
            src="/images/ant.png" 
            alt="ANT Design"
            />
          <HomeImage 
            src="/images/rea.png" 
            alt="React"
            />
          <HomeImage 
            src="/images/nex.png" 
            alt="NEXT.js" 
            />
          <HomeImage 
            src="/images/asp.png" 
            alt="ASP NET Core 9" 
            />
          <HomeImage 
            src="/images/ef.png" 
            alt="Entity Framework"
            />
          <HomeImage 
            src="/images/sql.png" 
            alt="Microsoft SQL Server" 
            />
        </div>

      </div>
    </div>
  );
}
